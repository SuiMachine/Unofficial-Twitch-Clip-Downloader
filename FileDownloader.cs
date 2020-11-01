using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public static void Download(int Iterator, TwitchVideo Video, string Directory_Path)
		{

			using (WebClient wb = new WebClient())
			{
				if (!Directory.Exists(Directory_Path))
					Directory.CreateDirectory(Directory_Path);

				var dateWinFormat = Video.CreationDate.ToString("yyyy.MM.dd");
				var safeGameTitle = GetIOSafePath(Video.Game);
				var safeClipName = GetIOSafePath(Video.ClipName);
				var download_path = Path.Combine(Directory_Path, string.Format("{0} - {1} - {2} - {3}.mp4", Iterator.ToString("00000"), dateWinFormat, safeGameTitle, safeClipName));
				while (File.Exists(download_path))
				{
					var dir = Path.GetDirectoryName(download_path);
					var filename = Path.GetFileNameWithoutExtension(download_path) + new Random().Next(0, 10);
					var extansion = Path.GetExtension(download_path);
					download_path = Path.Combine(dir, filename) + extansion;
				}

				wb.DownloadFile(Video.DownloadUri.ToString(), download_path);
			}
		}

		public static bool CheckIfExists(Uri file)
		{
			var request = (HttpWebRequest)WebRequest.Create(file);
			request.Method = "HEAD";
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				response.Close();
				return true;
			}
			catch (WebException exception)
			{
				Debug.WriteLine(exception);
				return false;
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
