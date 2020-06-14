using System;
using System.ComponentModel;
using System.Linq;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.label5 = new System.Windows.Forms.Label();
            this.NBS_ClipLimit = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CBox_SortBy = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.B_TwitchLogin = new System.Windows.Forms.Button();
            this.link_TwitchLegal = new System.Windows.Forms.LinkLabel();
            this.toolTip_Hint = new System.Windows.Forms.ToolTip(this.components);
            this.CB_SortDesc = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NBS_ClipLimit)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Twitch Username:";
            // 
            // TB_Username
            // 
            this.TB_Username.Location = new System.Drawing.Point(102, 3);
            this.TB_Username.Name = "TB_Username";
            this.TB_Username.Size = new System.Drawing.Size(287, 20);
            this.TB_Username.TabIndex = 2;
            // 
            // dateTimePicker_FromDateTime
            // 
            this.dateTimePicker_FromDateTime.Location = new System.Drawing.Point(42, 29);
            this.dateTimePicker_FromDateTime.Name = "dateTimePicker_FromDateTime";
            this.dateTimePicker_FromDateTime.Size = new System.Drawing.Size(347, 20);
            this.dateTimePicker_FromDateTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "To:";
            // 
            // dateTimePicker_ToDateTime
            // 
            this.dateTimePicker_ToDateTime.Location = new System.Drawing.Point(42, 55);
            this.dateTimePicker_ToDateTime.Name = "dateTimePicker_ToDateTime";
            this.dateTimePicker_ToDateTime.Size = new System.Drawing.Size(347, 20);
            this.dateTimePicker_ToDateTime.TabIndex = 6;
            // 
            // B_GetClips
            // 
            this.B_GetClips.Location = new System.Drawing.Point(314, 137);
            this.B_GetClips.Name = "B_GetClips";
            this.B_GetClips.Size = new System.Drawing.Size(75, 23);
            this.B_GetClips.TabIndex = 7;
            this.B_GetClips.Text = "Get clips";
            this.B_GetClips.UseVisualStyleBackColor = true;
            this.B_GetClips.Click += new System.EventHandler(this.B_GetClips_Click);
            // 
            // B_SaveUsername
            // 
            this.B_SaveUsername.Location = new System.Drawing.Point(215, 137);
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
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Download path:";
            // 
            // TB_DefaulthPath
            // 
            this.TB_DefaulthPath.Location = new System.Drawing.Point(91, 108);
            this.TB_DefaulthPath.Name = "TB_DefaulthPath";
            this.TB_DefaulthPath.Size = new System.Drawing.Size(217, 20);
            this.TB_DefaulthPath.TabIndex = 10;
            // 
            // B_BrowsePath
            // 
            this.B_BrowsePath.Location = new System.Drawing.Point(314, 108);
            this.B_BrowsePath.Name = "B_BrowsePath";
            this.B_BrowsePath.Size = new System.Drawing.Size(75, 23);
            this.B_BrowsePath.TabIndex = 11;
            this.B_BrowsePath.Text = "Browse";
            this.B_BrowsePath.UseVisualStyleBackColor = true;
            this.B_BrowsePath.Click += new System.EventHandler(this.B_BrowsePath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Clip limit:";
            // 
            // NBS_ClipLimit
            // 
            this.NBS_ClipLimit.Location = new System.Drawing.Point(56, 139);
            this.NBS_ClipLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NBS_ClipLimit.Name = "NBS_ClipLimit";
            this.NBS_ClipLimit.Size = new System.Drawing.Size(54, 20);
            this.NBS_ClipLimit.TabIndex = 13;
            this.toolTip_Hint.SetToolTip(this.NBS_ClipLimit, "Setting clip limit to 0 is equal to no limit (although it may take a significent " +
        "amount of time to gather clips due to multiple requests needed)");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.link_TwitchLegal, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 224);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CB_SortDesc);
            this.panel1.Controls.Add(this.CBox_SortBy);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.B_TwitchLogin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.NBS_ClipLimit);
            this.panel1.Controls.Add(this.TB_Username);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dateTimePicker_FromDateTime);
            this.panel1.Controls.Add(this.B_BrowsePath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TB_DefaulthPath);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker_ToDateTime);
            this.panel1.Controls.Add(this.B_SaveUsername);
            this.panel1.Controls.Add(this.B_GetClips);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 167);
            this.panel1.TabIndex = 0;
            // 
            // CBox_SortBy
            // 
            this.CBox_SortBy.FormattingEnabled = true;
            this.CBox_SortBy.Location = new System.Drawing.Point(52, 81);
            this.CBox_SortBy.Name = "CBox_SortBy";
            this.CBox_SortBy.Size = new System.Drawing.Size(248, 21);
            this.CBox_SortBy.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Sort by:";
            // 
            // B_TwitchLogin
            // 
            this.B_TwitchLogin.Location = new System.Drawing.Point(116, 137);
            this.B_TwitchLogin.Name = "B_TwitchLogin";
            this.B_TwitchLogin.Size = new System.Drawing.Size(93, 23);
            this.B_TwitchLogin.TabIndex = 14;
            this.B_TwitchLogin.Text = "Twitch Login";
            this.B_TwitchLogin.UseVisualStyleBackColor = true;
            this.B_TwitchLogin.Click += new System.EventHandler(this.B_TwitchLogin_Click);
            // 
            // link_TwitchLegal
            // 
            this.link_TwitchLegal.AutoSize = true;
            this.link_TwitchLegal.Location = new System.Drawing.Point(4, 176);
            this.link_TwitchLegal.Name = "link_TwitchLegal";
            this.link_TwitchLegal.Size = new System.Drawing.Size(377, 39);
            this.link_TwitchLegal.TabIndex = 2;
            this.link_TwitchLegal.TabStop = true;
            this.link_TwitchLegal.Text = resources.GetString("link_TwitchLegal.Text");
            // 
            // toolTip_Hint
            // 
            this.toolTip_Hint.IsBalloon = true;
            this.toolTip_Hint.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip_Hint.ToolTipTitle = "Hint";
            // 
            // CB_SortDesc
            // 
            this.CB_SortDesc.AutoSize = true;
            this.CB_SortDesc.Location = new System.Drawing.Point(306, 83);
            this.CB_SortDesc.Name = "CB_SortDesc";
            this.CB_SortDesc.Size = new System.Drawing.Size(83, 17);
            this.CB_SortDesc.TabIndex = 17;
            this.CB_SortDesc.Text = "Descending";
            this.CB_SortDesc.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 224);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Unofficial Twitch Clip Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NBS_ClipLimit)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void AddComboboxDataSources()
        {
            CBox_SortBy.DisplayMember = "Description";
            CBox_SortBy.ValueMember = "value";
            CBox_SortBy.DataSource = Enum.GetValues(typeof(SortByEnum)).Cast<Enum>().Select(value =>
            new
            {
                (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                value
            }).ToList();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown NBS_ClipLimit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel link_TwitchLegal;
        private System.Windows.Forms.Button B_TwitchLogin;
        private System.Windows.Forms.ToolTip toolTip_Hint;
        private System.Windows.Forms.ComboBox CBox_SortBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox CB_SortDesc;
    }
}

