using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TwitchClipDownloader.Forms
{
	public partial class LoginSettings : Form
	{
		public string BearerToken { get; set; }

		public LoginSettings(string BearerToken)
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;
			this.BearerToken = BearerToken;
			TB_Password.UseSystemPasswordChar = true;

			TB_Password.DataBindings.Add("Text", this, "BearerToken", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void CB_ShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (CB_ShowPassword.Checked)
				TB_Password.UseSystemPasswordChar = false;
			else
				TB_Password.UseSystemPasswordChar = true;
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void B_ObtainNewToken_Click(object sender, EventArgs e)
		{
			Process.Start("https://id.twitch.tv/oauth2/authorize?response_type=token" +
				"&client_id=llyidmsqifdz79kqkegwtv5lt11h2c" +
				"&redirect_uri=https://suimachine.github.io/twitchauthy/" +
				"&scope=openid");


			MessageBox.Show("Please authorize Clip Downloader on Twitch and then copy BearerToken to a field in application.");
		}
	}
}
