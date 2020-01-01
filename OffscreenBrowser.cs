using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public static class OffscreenBrowser
    {
        public static CefSharp.OffScreen.ChromiumWebBrowser Browser;


        public static void Initialize()
        {
            var browserSettings = new BrowserSettings();
            browserSettings.WindowlessFrameRate = 1;
            var requestContextSettings = new RequestContextSettings { CachePath = "Path1" };
            var requestContext = new RequestContext(requestContextSettings);
            Browser = new CefSharp.OffScreen.ChromiumWebBrowser("https://twitch.tv", browserSettings, requestContext);
        }
    }
}
