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
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config = ApplicationConfig.Load();

            //Setup dates
            FromDateTime = DateTime.UtcNow - TimeSpan.FromDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));
            ToDateTime = DateTime.UtcNow;

            //AddCombobox sources
            this.AddComboboxDataSources();

            //Setup bindings
            TB_Username.DataBindings.Add("Text", Config, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);
            TB_DefaulthPath.DataBindings.Add("Text", Config, "FilePath", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_FromDateTime.DataBindings.Add("Value", this, "FromDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_ToDateTime.DataBindings.Add("Value", this, "ToDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            NBS_ClipLimit.DataBindings.Add("Value", Config, "ClipLimit", false, DataSourceUpdateMode.OnPropertyChanged);
            CBox_SortBy.DataBindings.Add("SelectedValue", Config, "SortBy", false, DataSourceUpdateMode.OnPropertyChanged);
            CB_SortDesc.DataBindings.Add("Checked", Config, "SortByDesc", false, DataSourceUpdateMode.OnPropertyChanged);

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
            if(Config.BearerToken == "")
            {
                MessageBox.Show("Since May 2020, Twitch requires Bearer token for HTTP requests. Please proceed to Login and obtain your own Twitch Login Token to use with Clip Downloader.");
            }
            else
            {
                JsonGrabber.ProvideBearerToken(Config.BearerToken);
                //We don't want to deal with time - just dates, so round it up to first second of the day for "FromDateTime" and last second of the day for ToDateTime, this way if we choose same day in both fields, we pretty much ned up with entire day selected... UTC time.
                FromDateTime = FromDateTime.Date;
                ToDateTime = ToDateTime.Date.AddDays(1).Subtract(TimeSpan.FromSeconds(1));

                VideoPicker vpicker = new VideoPicker(Config.UserName, Config.FilePath, Config.ClipLimit, FromDateTime, ToDateTime, (SortByEnum)CBox_SortBy.SelectedValue, CB_SortDesc.Checked);
                vpicker.ShowDialog();
            }
        }

        private void B_TwitchLogin_Click(object sender, EventArgs e)
        {
            Forms.LoginSettings lgfrm = new Forms.LoginSettings(Config.BearerToken);
            var result = lgfrm.ShowDialog();
            if(result == DialogResult.OK)
            {
                if(lgfrm.BearerToken != "")
                {
                    Config.BearerToken = lgfrm.BearerToken;
                    Config.Save();
                }
            }
        }
    }
}
