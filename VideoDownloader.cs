using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwitchClipDownloader
{
    class VideoDownloader
    {
        VideoPicker Vp { get; set; }

        public VideoDownloader(VideoPicker Vp)
        {
            this.Vp = Vp;
        }

        public ulong? GetBroadcasterID(string BroadcasterName)
        {
            Vp.InvokeStatusUpdate("Getting broadcaster ID...", Color.Green);
            if (JsonGrabber.GrabJson(
                JsonGrabber.GetPathHelix("users", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("login", BroadcasterName) }),
                "application/json",
                "application/vnd.twitchtv.v3+json",
                "GET",
                out string res))
            {
                var userData = JsonConvert.DeserializeXmlNode(res)["data"];
                if (userData == null)
                    return null;
                var id = userData["id"];
                return ulong.Parse(id.InnerText);
            }

            return null;
        }

        public TwitchVideo[] GetVideos(ulong BroadcasterID, uint ClipLimit, DateTime FromDate, DateTime ToDate)
        {
            bool isMoreThan100Clips = ClipLimit > 100 || ClipLimit == 0; //Currently not used

            var QueryParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("broadcaster_id", BroadcasterID.ToString()) };
            if(FromDate != null && ToDate != null)
            {
                QueryParams.Add(new KeyValuePair<string, string>("started_at", FromDate.ToRfc3339String()));
                QueryParams.Add(new KeyValuePair<string, string>("ended_at", ToDate.ToRfc3339String()));
            }
            if(isMoreThan100Clips)
                QueryParams.Add(new KeyValuePair<string, string>("first", "100"));
            else
                QueryParams.Add(new KeyValuePair<string, string>("first", (ClipLimit +1).ToString()));  //Cause sometimes it returns 1 less

            Vp.InvokeStatusUpdate("Getting list of clips...", System.Drawing.Color.Green);
            if (JsonGrabber.GrabJson(
                JsonGrabber.GetPathHelix("clips", QueryParams ),
                "application/json",
                "application/vnd.twitchtv.v3+json",
                "GET",
                out string res))
            {
                Vp.InvokeStatusUpdate("Received response, parsing JSON...", System.Drawing.Color.Green);
                var response = JObject.Parse(res);
                var dataNode = response.Children().FirstOrDefault(x => x.Path == "data");
                if (dataNode != null)
                {
                    var videosNode = dataNode.Children().First();

                    List<TwitchVideo> vids = new List<TwitchVideo>();
                    if (isMoreThan100Clips && videosNode.Count() > 75)
                    {
                        var timeRange = ((ToDate - FromDate).TotalMinutes) / 2;
                        vids.AddRange(GetVideos(BroadcasterID, 0, FromDate, FromDate + TimeSpan.FromMinutes(timeRange)));
                        vids.AddRange(GetVideos(BroadcasterID, 0, FromDate + TimeSpan.FromMinutes(timeRange), ToDate));
                    }
                    else
                    {
                        for (int i = 0; i < videosNode.Children().Count(); i++)
                        {
                            var id = videosNode.Children().ElementAt(i)["id"].ToString();
                            var date = videosNode.Children().ElementAt(i)["created_at"].ToString();
                            var game = videosNode.Children().ElementAt(i)["game_id"].ToString();
                            var title = videosNode.Children().ElementAt(i)["title"].ToString();
                            var previewLink = videosNode.Children().ElementAt(i)["url"].ToString();
                            vids.Add(new TwitchVideo(id, date, title, game, new Uri(previewLink), null));
                        }
                    }

                    return vids.ToArray();
                }
                else
                {
                    Vp.InvokeStatusUpdate("No videos were found in HTTP response...", System.Drawing.Color.Red);
                    return new TwitchVideo[0];
                }
            }
            Vp.InvokeStatusUpdate("Failed to receive response...", System.Drawing.Color.Red);
            return null;
        }

        public void TranslateGameIDsToNames(TwitchVideo[] vids)
        {
            List<string> gameIDs = new List<string>();
            for(int i=0; i<vids.Length; i++)
            {
                if(!gameIDs.Contains(vids[i].Game))
                    gameIDs.Add(vids[i].Game);
            }


            var translationDictionary = this.GetIDToGameNameDictionary(gameIDs.ToArray());

            //Translate IDs to Game Names
            if (translationDictionary != null)
            {
                foreach (var vid in vids)
                {
                    //Translate IDs to game titles
                    if (translationDictionary.ContainsKey(vid.Game))
                        vid.Game = translationDictionary[vid.Game];
                }
            }
        }

        public void GetDownloadLinks(TwitchVideo[] vids)
        {
            int i = 1;
            foreach(var vid in vids)
            {
                Vp.InvokeStatusUpdate(string.Format("Getting download link for video {0} / {1}", i, vids.Length), System.Drawing.Color.Green);
                vid.DownloadUri = new Uri(GetDownloadLink(vid.ID));
                i++;
            }
        }

        private Dictionary<string, string> GetIDToGameNameDictionary(string[] gameIDsArray)
        {
            Vp.InvokeStatusUpdate("Getting game names to translate game IDs...", System.Drawing.Color.Green);
            var querryStrings = new List<KeyValuePair<string, string>>() { };
            foreach(var gameId in gameIDsArray)
            {
                querryStrings.Add(new KeyValuePair<string, string>("id", gameId));
            }


            if(JsonGrabber.GrabJson(JsonGrabber.GetPathHelix("games", querryStrings),
                "application/json",
                "application/vnd.twitchtv.v3+json",
                "GET",
                out string res))
            {
                Vp.InvokeStatusUpdate("Received response, parsing JSON...", System.Drawing.Color.Green);
                var response = JObject.Parse(res);
                var dataNode = response.Children().FirstOrDefault(x => x.Path == "data");
                if (dataNode != null)
                {
                    var gamesNode = dataNode.Children().First();
                    Dictionary<string, string> returnDic = new Dictionary<string, string>();

                    foreach (var gameNode in gamesNode.Children())
                    {
                        var id = gameNode["id"].ToString();
                        var name = gameNode["name"].ToString();
                        if(!returnDic.ContainsKey(id))
                        {
                            returnDic.Add(id, name);
                        }
                    }
                    return returnDic;
                    
                }
                Vp.InvokeStatusUpdate("Data node for game IDs was empty!", Color.Red);
            }
            return new Dictionary<string, string>();
        }

        private string GetDownloadLink(string SLUG)
        {
            if (JsonGrabber.GrabClipJson(SLUG, out string res))
            {
                return res;
            }

            return "ERROR";
        }
    }
}
