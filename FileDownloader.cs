using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    static class FileDownloader
    {
        public static void Download(TwitchVideo Video, string Directory)
        {

            using(WebClient wb = new WebClient())
            {
                var dateWinFormat = DateTime.Parse(Video.CreationDate).ToString("yyyy.MM.dd");
                var safeGameTitle = GetIOSafePath(Video.Game);
                var safeClipName = GetIOSafePath(Video.ClipName);
                wb.DownloadFile(Video.DownloadUri.ToString(), Path.Combine(Directory, string.Format("{0} - {1} - {2}.mp4", dateWinFormat, safeGameTitle, safeClipName)));
            }
        }

        public static string GetIOSafePath(string name)
        {
            // first trim the raw string
            string safe = name.Trim();
            // replace spaces with hyphens
            safe = safe.Replace(" ", "-").ToLower();
            // replace any 'double spaces' with singles
            if (safe.IndexOf("--") > -1)
                while (safe.IndexOf("--") > -1)
                    safe = safe.Replace("--", "-");
            // trim out illegal characters
            safe = Regex.Replace(safe, "[^a-z0-9\\-]", "");
            // trim the length
            if (safe.Length > 50)
                safe = safe.Substring(0, 49);
            // clean the beginning and end of the filename
            char[] replace = { '-', '.' };
            safe = safe.TrimStart(replace);
            safe = safe.TrimEnd(replace);
            return safe;
        }
    }
}
