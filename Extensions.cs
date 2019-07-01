using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    static class Extensions
    {
        public static string ToRfc3339String(this DateTime dateTime)
        {
            //2017-11-30T22:34:18Z
            return dateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", DateTimeFormatInfo.InvariantInfo);
        }
    }
}
