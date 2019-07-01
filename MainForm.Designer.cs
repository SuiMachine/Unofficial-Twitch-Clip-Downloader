namespace TwitchClipDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Username = new System.Windows.Forms.TextBox();
            this.dateTimePicker_FromDateTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker_ToDateTime = new System.Windows.Forms.DateTimePicker();
            this.B_GetClips = new System.Windows.Forms.Button();
            this.B_SaveUsername = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_DefaulthPath = new System.Windows.Forms.TextBox();
            this.B_BrowsePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Twitch Username:";
            // 
            // TB_Username
            // 
            this.TB_Username.Location = new System.Drawing.Point(111, 6);
            this.TB_Username.Name = "TB_Username";
            this.TB_Username.Size = new System.Drawing.Size(287, 20);
            this.TB_Username.TabIndex = 2;
            // 
            // dateTimePicker_FromDateTime
            // 
            this.dateTimePicker_FromDateTime.Location = new System.Drawing.Point(51, 32);
            this.dateTimePicker_FromDateTime.Name = "dateTimePicker_FromDateTime";
            this.dateTimePicker_FromDateTime.Size = new System.Drawing.Size(347, 20);
            this.dateTimePicker_FromDateTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "To:";
            // 
            // dateTimePicker_ToDateTime
            // 
            this.dateTimePicker_ToDateTime.Location = new System.Drawing.Point(51, 58);
            this.dateTimePicker_ToDateTime.Name = "dateTimePicker_ToDateTime";
            this.dateTimePicker_ToDateTime.Size = new System.Drawing.Size(347, 20);
            this.dateTimePicker_ToDateTime.TabIndex = 6;
            // 
            // B_GetClips
            // 
            this.B_GetClips.Location = new System.Drawing.Point(323, 109);
            this.B_GetClips.Name = "B_GetClips";
            this.B_GetClips.Size = new System.Drawing.Size(75, 23);
            this.B_GetClips.TabIndex = 7;
            this.B_GetClips.Text = "Get clips";
            this.B_GetClips.UseVisualStyleBackColor = true;
            this.B_GetClips.Click += new System.EventHandler(this.B_GetClips_Click);
            // 
            // B_SaveUsername
            // 
            this.B_SaveUsername.Location = new System.Drawing.Point(12, 109);
            this.B_SaveUsername.Name = "B_SaveUsername";
            this.B_SaveUsername.Size = new System.Drawing.Size(93, 23);
            this.B_SaveUsername.TabIndex = 8;
            this.B_SaveUsername.Text = "Save settings";
            this.B_SaveUsername.UseVisualStyleBackColor = true;
            this.B_SaveUsername.Click += new System.EventHandler(this.B_SaveUsername_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Default download path:";
            // 
            // TB_DefaulthPath
            // 
            this.TB_DefaulthPath.Location = new System.Drawing.Point(135, 83);
            this.TB_DefaulthPath.Name = "TB_DefaulthPath";
            this.TB_DefaulthPath.Size = new System.Drawing.Size(182, 20);
            this.TB_DefaulthPath.TabIndex = 10;
            // 
            // B_BrowsePath
            // 
            this.B_BrowsePath.Location = new System.Drawing.Point(323, 81);
            this.B_BrowsePath.Name = "B_BrowsePath";
            this.B_BrowsePath.Size = new System.Drawing.Size(75, 23);
            this.B_BrowsePath.TabIndex = 11;
            this.B_BrowsePath.Text = "Browse";
            this.B_BrowsePath.UseVisualStyleBackColor = true;
            this.B_BrowsePath.Click += new System.EventHandler(this.B_BrowsePath_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 140);
            this.Controls.Add(this.B_BrowsePath);
            this.Controls.Add(this.TB_DefaulthPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.B_SaveUsername);
            this.Controls.Add(this.B_GetClips);
            this.Controls.Add(this.dateTimePicker_ToDateTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker_FromDateTime);
            this.Controls.Add(this.TB_Username);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Twitch Clip Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Username;
        private System.Windows.Forms.DateTimePicker dateTimePicker_FromDateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker_ToDateTime;
        private System.Windows.Forms.Button B_GetClips;
        private System.Windows.Forms.Button B_SaveUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_DefaulthPath;
        private System.Windows.Forms.Button B_BrowsePath;
    }
}

