using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public DateTime FromDateTime { get; private set; }
        public DateTime ToDateTime { get; private set; }



        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config = ApplicationConfig.Load();

            //Setup dates
#if DEBUG && FALSE
            FromDateTime = DateTime.UtcNow - TimeSpan.FromDays(365);
#else
            FromDateTime = DateTime.UtcNow - TimeSpan.FromDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));
#endif
            ToDateTime = DateTime.UtcNow;

            //Setup bindings
            TB_Username.DataBindings.Add("Text", Config, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);
            TB_DefaulthPath.DataBindings.Add("Text", Config, "FilePath", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_FromDateTime.DataBindings.Add("Value", this, "FromDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker_ToDateTime.DataBindings.Add("Value", this, "ToDateTime", false, DataSourceUpdateMode.OnPropertyChanged);
            NBS_ClipLimit.DataBindings.Add("Value", Config, "ClipLimit", false, DataSourceUpdateMode.OnPropertyChanged);
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
            VideoPicker vpicker = new VideoPicker(Config.UserName, Config.FilePath, Config.ClipLimit, FromDateTime, ToDateTime);
            vpicker.ShowDialog();


        }
    }
}
