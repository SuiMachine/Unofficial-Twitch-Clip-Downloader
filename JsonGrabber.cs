using CefSharp;
using CefSharp.OffScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchClipDownloader
{
    internal static class JsonGrabber
    {
        static KeyValuePair<string, string> clientIDHeader = new KeyValuePair<string, string>("Client-ID", "llyidmsqifdz79kqkegwtv5lt11h2c");
        static KeyValuePair<string, string> BearerToken;
        static DateTime lastRequest = DateTime.UtcNow;

        public static void ProvideBearerToken(string Token)
        {
            BearerToken = new KeyValuePair<string, string>("Authorization", "Bearer " + Token.Trim());
        }

        public static Uri GetPathHelix(string endpoint, List<KeyValuePair<string, string>> queryStrings = null)
        {
            return GetPathHelix(new string[1] { endpoint }, queryStrings);
        }

        public static Uri GetPathHelix(string[] endpoint, List<KeyValuePair<string, string>> queryStrings = null)
        {
            if(queryStrings != null)
            {
                var first = queryStrings.ElementAt(0);
                string build = "?" + first.Key + "=" + first.Value;
                for(int i=1; i<queryStrings.Count; i++)
                {
                    build += "&" + queryStrings.ElementAt(i).Key + "=" + queryStrings.ElementAt(i).Value;
                }
                return new Uri("https://api.twitch.tv/helix/" + string.Join("/", endpoint) + build);
            }
            else
                return new Uri("https://api.twitch.tv/helix/" + string.Join("/", endpoint));
        }

        private static string lastSLUG = "";

        public static bool GrabClipJson(string SLUG,  out string result)
        {
            Uri address = new Uri("https://clips.twitch.tv/embed?clip=" + SLUG);


            var sleep = (lastRequest + TimeSpan.FromSeconds(1) - DateTime.UtcNow).TotalMilliseconds;
            if (sleep > 0)
                System.Threading.Thread.Sleep((int)sleep);
            try
            {
                lastRequest = DateTime.UtcNow;

                result = GetBestQualityFromPage(address.ToString());
                lastSLUG = SLUG;
                return true;
            }
            catch(Exception e)
            {
                result = "";
                lastRequest = DateTime.UtcNow;
                return false;
            }
        }

        private static string GetBestQualityFromPage(string address)
        {
            using(var OffscreenBrowser = new OffscreenBrowser())
            {
                string pageSource;
                while (!OffscreenBrowser.Browser.IsBrowserInitialized)
                    System.Threading.Thread.Sleep(1);


                OffscreenBrowser.Browser.Load(address);


                while (OffscreenBrowser.Browser.IsLoading)
                    System.Threading.Thread.Sleep(100);

                while (! (pageSource = OffscreenBrowser.Browser.GetSourceAsync().GetAwaiter().GetResult()).Contains(".mp4"))
                    System.Threading.Thread.Sleep(500);

                var mp4End = pageSource.IndexOf(".mp4") + 4;
                pageSource = pageSource.Substring(0, mp4End);
                var mp4Begin = pageSource.LastIndexOf("http");
                var clipUrl = pageSource.Substring(mp4Begin);
                return clipUrl;


                //This is broken now, cause Twitch is fuckin' shit company

                /*
                var qualities = OffscreenBrowser.Browser.EvaluateScriptAsync("player.getQualities()").GetAwaiter().GetResult();
                var results = (List<object>)qualities.Result;

                int bestQuality = 0;
                var best = (dynamic)results.First();

                for (int i = 0; i < results.Count; i++)
                {
                    var quality = (dynamic)results[i];
                    string name = quality.name;
                    if (name.EndsWith("p"))
                        name = name.Remove(name.LastIndexOf("p"));
                    int qualityNumber = 0;

                    if (!int.TryParse(name, out qualityNumber))
                    {
                        MessageBox.Show(string.Format("Can't parse quality name for {0}. Quality = {1}", address, name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "Error";
                    }

                    if (qualityNumber > bestQuality)
                    {
                        bestQuality = qualityNumber;
                        best = quality;
                    }
                }

                return best.source;*/
            }
        }

        private static string GetPageSource(ChromiumWebBrowser browser)
        {
            var getPage = browser.GetSourceAsync();
            while(!getPage.IsCompleted)
            {
                System.Threading.Thread.Sleep(2000);
            }

            if (getPage.IsCompleted)
                return getPage.GetAwaiter().GetResult();
            else
                return "";
        }

        public static bool GrabJson(Uri address, string contantType, string acceptStr, string Method, out string result)
        {
            return GrabJson(address, new Dictionary<string, string>(), contantType, acceptStr, Method, out result);
        }

        public static bool GrabJson(Uri address, Dictionary<string, string> headers, string contantType, string acceptStr, string Method, out string result)
        {
            var sleep = (lastRequest + TimeSpan.FromSeconds(1) - DateTime.UtcNow).TotalMilliseconds;
            if (sleep > 0)
                System.Threading.Thread.Sleep((int)sleep);

            try
            {
                HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create(address);
                headers.Add(clientIDHeader.Key, clientIDHeader.Value);
                headers.Add(BearerToken.Key, BearerToken.Value);

                //Headers
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        wRequest.Headers[header.Key] = header.Value;
                    }
                }

                //ConstantType
                if (contantType != null)
                {
                    wRequest.ContentType = contantType;
                }

                //AcceptString
                if (acceptStr != null)
                {
                    wRequest.Accept = acceptStr;
                }

                //Method
                if (Method != null)
                {
                    wRequest.Method = Method;
                }

                dynamic wResponse = wRequest.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(wResponse);
                result = reader.ReadToEnd();
                reader.Close();
                wResponse.Close();
                lastRequest = DateTime.UtcNow;
                return true;
            }
            catch(Exception e)
            {
                result = "";
                lastRequest = DateTime.UtcNow;
                return false;
            }
        }
    }
}
