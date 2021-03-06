﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchClipDownloader
{
	public partial class VideoPicker : Form
	{
		private readonly string UserName;
		public string Path { get; set; }

		private readonly uint ClipLimit;
		private readonly DateTime FromDateTime;
		private readonly DateTime ToDateTime;
		private TwitchVideo[] videos;
		private readonly SortByEnum SortBy;
		private readonly bool SortByDesc;

		Thread workerThread;
		public delegate void StatusUpdateDelagate(string text, Color color);
		public delegate void SetupTableDelegate(TwitchVideo[] videos);
		public delegate void SetCompleteDelegate();

		public VideoPicker(string UserName, string Path, uint ClipLimit, DateTime FromDateTime, DateTime ToDateTime, SortByEnum SortBy, bool SortByDesc)
		{
			InitializeComponent();
			this.UserName = UserName;
			this.Path = Path;
			this.ClipLimit = ClipLimit;
			this.FromDateTime = FromDateTime;
			this.ToDateTime = ToDateTime;
			this.SortBy = SortBy;
			this.SortByDesc = SortByDesc;
		}

		private void VideoPicker_Load(object sender, EventArgs e)
		{
			TB_Directory.DataBindings.Add("Text", this, "path", false, DataSourceUpdateMode.OnPropertyChanged);
			this.B_Download.Enabled = false;

			workerThread = new Thread(PerformGetVideosThread);
			workerThread.Start();
		}

		public void InvokeStatusUpdate(string statusText, Color textColor)
		{
			if (this.LB_Status.InvokeRequired)
			{
				StatusUpdateDelagate d = new StatusUpdateDelagate(InvokeStatusUpdate);
				this.Invoke(d, new object[] { statusText, textColor });
			}
			else
			{
				this.LB_Status.Text = statusText;
				this.LB_Status.ForeColor = textColor;
			}
		}

		public void InvokeSetupTable(TwitchVideo[] videos)
		{
			if (this.panel_Content.InvokeRequired)
			{
				SetupTableDelegate d = new SetupTableDelegate(InvokeSetupTable);
				this.Invoke(d, new object[] { videos });
			}
			else
			{
				TableLayoutPanel panel = new TableLayoutPanel() { Dock = DockStyle.Fill, AutoScroll = true, CellBorderStyle = TableLayoutPanelCellBorderStyle.Single, ColumnCount = 5 };

				var controls = panel.Controls;
				controls.Clear();
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
				controls.Add(new Label() { Text = "Date" }, 0, 0);

				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
				controls.Add(new Label() { Text = "Title" }, 1, 0);

				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
				controls.Add(new Label() { Text = "Game" }, 2, 0);

				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
				controls.Add(new Label() { Text = "Preview" }, 3, 0);

				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
				var checkDownload = new CheckBox() { Text = "Download" };
				checkDownload.CheckedChanged += (object snd, EventArgs eArgs) =>
				{
					for (int i = 1; i < panel.RowCount; i++)
					{
						//Stupid binding doesn't work :-/
						var elementFromTable = panel.GetControlFromPosition(4, i);
						if (elementFromTable.GetType() == typeof(CheckBox))
						{
							var cast = (CheckBox)elementFromTable;
							cast.Checked = checkDownload.Checked;
						}
					}
				};
				controls.Add(checkDownload, 4, 0);


				for (int i = 0; i < videos.Length; i++)
				{
					controls.Add(new Label() { Text = videos[i].CreationDate.ToString("yyyy.MM.dd") }, 0, i + 1);
					controls.Add(new Label() { Text = videos[i].ClipName, Dock = DockStyle.Top }, 1, i + 1);
					controls.Add(new Label() { Text = videos[i].Game, Dock = DockStyle.Top }, 2, i + 1);

					//preview button
					var button = new Button() { Text = "", Name = videos[i].PreviewUri.ToString() };
					button.Click += (sender, e) =>
					{
						Process.Start(button.Name);
					};
					controls.Add(button, 3, i + 1);

					//checkbox
					var downloadCB = new CheckBox() { Text = "" };
					downloadCB.DataBindings.Add("Checked", videos[i], "Download", false, DataSourceUpdateMode.OnPropertyChanged);
					controls.Add(downloadCB, 4, i + 1);
					panel.RowCount = i + 2;
				}

				panel_Content.Controls.Add(panel);
			}
		}


		private void PerformGetVideosThread()
		{
			VideoDownloader vd = new VideoDownloader(this);

			var twitchUsernameId = vd.GetBroadcasterID(UserName);

			//Something went wrong when obtaining Twitch username ID!
			if (twitchUsernameId == null)
			{
				InvokeStatusUpdate("Failed to obtain broadcaster ID.", Color.Red);
				MessageBox.Show("Failed to obtain Twitch Username ID. Twitch username may be invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			//It's recuresive now to make sure it downloads all videos
			videos = vd.GetVideos((ulong)twitchUsernameId, ClipLimit, FromDateTime, ToDateTime);

			//Trim the list to only specified amount of clips
			if (ClipLimit != 0)
			{
				var arrayClipAmount = ClipLimit > videos.Length ? (uint)videos.Length : ClipLimit;
				TwitchVideo[] vidTemp = new TwitchVideo[arrayClipAmount];
				for (int i = 0; i < vidTemp.Length; i++)
				{
					vidTemp[i] = videos[i];
				}
				videos = vidTemp;
			}

			//Used to be part of GetVideos, but was moved outside in order to make the recuresive function fast
			vd.GetDownloadLinks(videos);
			vd.TranslateGameIDsToNames(videos);

			//Sort by time
			InvokeStatusUpdate("Sorting...", Color.Blue);
			switch (SortBy)
			{
				case SortByEnum.CreationDate:
					videos = SortByDesc ? videos.OrderByDescending(x => x.CreationDate).ToArray() : videos.OrderBy(x => x.CreationDate).ToArray();
					break;
				case SortByEnum.Name:
					videos = SortByDesc ? videos.OrderByDescending(x => x.ClipName).ToArray() : videos.OrderBy(x => x.ClipName).ToArray();
					break;
				case SortByEnum.Views:
					videos = SortByDesc ? videos.OrderByDescending(x => x.View_count).ToArray() : videos.OrderBy(x => x.View_count).ToArray();
					break;
				default:
					videos = SortByDesc ? videos.OrderByDescending(x => x.CreationDate).ToArray() : videos.OrderBy(x => x.CreationDate).ToArray();
					break;
			}

			//Sets up table with videos
			InvokeSetupTable(videos);


			InvokeSetComplete();
			InvokeStatusUpdate("Done. Awaiting user input.", Color.DarkGray);
		}

		private void InvokeSetComplete()
		{
			if (B_Download.InvokeRequired)
			{
				SetCompleteDelegate d = new SetCompleteDelegate(InvokeSetComplete);
				this.Invoke(d, new object[] { });
			}
			else
			{
				this.B_Download.Enabled = true;
			}
		}

		private void CloseWindow()
		{
			if (workerThread != null && workerThread.ThreadState == System.Threading.ThreadState.Running)
			{
				workerThread.Abort();
			}
		}

		private void DownloadThread()
		{
			List<TwitchVideo> downloadList = new List<TwitchVideo>();
			foreach (var video in videos)
			{
				if (video.Download)
					downloadList.Add(video);
			}

			for (int i = 0; i < downloadList.Count; i++)
			{
				InvokeStatusUpdate(string.Format("Downloading video {0} / {1}", i + 1, downloadList.Count), Color.DarkGray);
				FileDownloader.Download(i, downloadList[i], Path);
			}

			InvokeStatusUpdate("Done. Awaiting user input.", Color.DarkGray);
			Process.Start(Path);
			MessageBox.Show("Done!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
			InvokeSetComplete();
		}

		private void B_Download_Click(object sender, EventArgs e)
		{
			workerThread = new Thread(DownloadThread);
			workerThread.Start();
			B_Download.Enabled = false;
		}

		private void VideoPicker_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseWindow();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			CloseWindow();
			this.Close();
		}
	}
}
