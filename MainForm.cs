using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TwitchClipDownloader
{
    public partial class MainForm : Form
    {
        public ApplicationConfig Config { get; private set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

        public MainForm()
        {
            OffscreenBrowserStatic.Initialize();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config = ApplicationConfig.Load();

            //Setup dates
            FromDateTime = DateTime.UtcNow - TimeSpan.FromDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));
            ToDateTime = DateTime.UtcNow;

#if DEBUG && FALSE
            FromDateTime = DateTime.Parse("01.12.2019 00:04:15");
            ToDateTime = DateTime.Parse("01.12.2019 23:04:15");
#endif

            //Setup bindings
            TB_Username.DataBindings.Add("Text", Config, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);
            TB_DefaulthPath.DataBindings.Add("Text", Config, "FilePath", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_FromDateTime.DataBindings.Add("Value", this, "FromDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_ToDateTime.DataBindings.Add("Value", this, "ToDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            NBS_ClipLimit.DataBindings.Add("Value", Config, "ClipLimit", false, DataSourceUpdateMode.OnPropertyChanged);

            //Legal link stuff
            link_TwitchLegal.LinkClicked += (send, arg) => { Process.Start("https://www.twitch.tv/p/legal/trademark/"); };
            int linkPos = link_TwitchLegal.Text.IndexOf("http");
            link_TwitchLegal.LinkArea = new LinkArea(linkPos, link_TwitchLegal.Text.Length - linkPos);
        }

        private void B_SaveUsername_Click(object sender, EventArgs e)
        {
            Config.Save();
        }

        private void B_BrowsePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog
            {
                SelectedPath = Config.FilePath,
                ShowNewFolderButton = true,
                Description = "Select default download folder for clips"
            };

            var result = fd.ShowDialog();
            if(result == DialogResult.OK)
            {
                Config.FilePath = fd.SelectedPath;
            }
        }

        private void B_GetClips_Click(object sender, EventArgs e)
        {
            //We don't want to deal with time - just dates, so round it up to first second of the day for "FromDateTime" and last second of the day for ToDateTime, this way if we choose same day in both fields, we pretty much ned up with entire day selected... UTC time.
            FromDateTime = FromDateTime.Date;
            ToDateTime = ToDateTime.Date.AddDays(1).Subtract(TimeSpan.FromSeconds(1));

            VideoPicker vpicker = new VideoPicker(Config.UserName, Config.FilePath, Config.ClipLimit, FromDateTime, ToDateTime);
            vpicker.ShowDialog();


        }
    }
}
