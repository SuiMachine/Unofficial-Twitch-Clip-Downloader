using System;
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
        string UserName;
        public string Path { get; set; }
        uint ClipLimit;
        DateTime FromDateTime;
        DateTime ToDateTime;
        TwitchVideo[] videos;

        Thread workerThread;
        public delegate void StatusUpdateDelagate(string text, Color color);
        public delegate void SetupTableDelegate(TwitchVideo[] videos);
        public delegate void SetCompleteDelegate();



        public VideoPicker(string UserName, string Path, uint ClipLimit, DateTime FromDateTime, DateTime ToDateTime)
        {
            InitializeComponent();
            this.UserName = UserName;
            this.Path = Path;
            this.ClipLimit = ClipLimit;
            this.FromDateTime = FromDateTime;
            this.ToDateTime = ToDateTime;
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
            if(this.LB_Status.InvokeRequired)
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
            if(this.panel_Content.InvokeRequired)
            {
                SetupTableDelegate d = new SetupTableDelegate(InvokeSetupTable);
                this.Invoke(d, new object[] { videos });
            }
            else
            {
                TableLayoutPanel panel = new TableLayoutPanel() { Dock = DockStyle.Fill, AutoScroll = true, CellBorderStyle = TableLayoutPanelCellBorderStyle.Single };

                var controls = panel.Controls;
                controls.Clear();
                controls.Add(new Label() { Text = "Date / Time" }, 0, 0);
                controls.Add(new Label() { Text = "Title" }, 1, 0);
                controls.Add(new Label() { Text = "Game" }, 2, 0);
                controls.Add(new Label() { Text = "Preview" }, 3, 0);
                controls.Add(new Label() { Text = "Download" }, 4, 0);


                for (int i = 0; i < videos.Length; i++)
                {
                    controls.Add(new Label() { Text = videos[i].CreationDate }, 0, i + 1);
                    controls.Add(new Label() { Text = videos[i].ClipName }, 1, i + 1);
                    controls.Add(new Label() { Text = videos[i].Game }, 2, i + 1);

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

            videos = vd.GetVideos((ulong)twitchUsernameId, ClipLimit, FromDateTime, ToDateTime);
            InvokeSetupTable(videos);
            InvokeSetComplete();
            InvokeStatusUpdate("Done. Awaiting user input.", Color.DarkGray);
        }

        private void InvokeSetComplete()
        {
            InvokeStatusUpdate("Building download list...", Color.DarkGray);

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

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            if(workerThread != null)
            {
                workerThread.Abort();
            }

            this.Close();
        }

        private void DownloadThread()
        {
            List<TwitchVideo> downloadList = new List<TwitchVideo>();
            foreach( var video in videos)
            {
                if(video.Download)
                    downloadList.Add(video);
            }

            for(int i=0; i<downloadList.Count; i++)
            {
                InvokeStatusUpdate(string.Format("Downloading video {0} / {1}", i + 1, downloadList.Count), Color.DarkGray);
                FileDownloader.Download(downloadList[i], Path);
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
    }
}
