using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    static class FileDownloader
    {
        public static void Download(Uri FileUri, string Directory)
        {
            var filename = FileUri.Segments.Last();
            using(WebClient wb = new WebClient())
            {
                wb.DownloadFile(FileUri.ToString(), Path.Combine(Directory, filename));
            }
        }
    }
}
