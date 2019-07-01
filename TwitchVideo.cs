using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public class TwitchVideo
    {
        public bool Download { get; set; }
        public Uri DownloadUri { get; set; }
        public Uri PreviewUri { get; set; }

        public string CreationDate { get; set; }
        public string ClipName { get; set; }
        public string Game { get; set; }
        public string ID { get; set; }


        public static readonly int COLUMNCOUNT = 5;

        public TwitchVideo(string ID, string CreationDate, string ClipName, string Game, Uri PreviewUri, Uri DownloadUri)
        {
            this.ID = ID;
            this.CreationDate = CreationDate;
            this.ClipName = ClipName;
            this.Game = Game;
            this.PreviewUri = PreviewUri;
            this.DownloadUri = DownloadUri;
            this.Download = true;
        }
    }
}
