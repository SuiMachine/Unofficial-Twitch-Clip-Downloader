namespace TwitchClipDownloader.Forms
{
    partial class LoginSettings
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
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.CB_ShowPassword = new System.Windows.Forms.CheckBox();
            this.B_ObtainNewToken = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bearer Token:";
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(89, 7);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.Size = new System.Drawing.Size(368, 20);
            this.TB_Password.TabIndex = 0;
            // 
            // CB_ShowPassword
            // 
            this.CB_ShowPassword.AutoSize = true;
            this.CB_ShowPassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CB_ShowPassword.Location = new System.Drawing.Point(11, 37);
            this.CB_ShowPassword.Name = "CB_ShowPassword";
            this.CB_ShowPassword.Size = new System.Drawing.Size(102, 17);
            this.CB_ShowPassword.TabIndex = 2;
            this.CB_ShowPassword.Text = "Show Password";
            this.CB_ShowPassword.UseVisualStyleBackColor = true;
            this.CB_ShowPassword.CheckedChanged += new System.EventHandler(this.CB_ShowPassword_CheckedChanged);
            // 
            // B_ObtainNewToken
            // 
            this.B_ObtainNewToken.Location = new System.Drawing.Point(348, 33);
            this.B_ObtainNewToken.Name = "B_ObtainNewToken";
            this.B_ObtainNewToken.Size = new System.Drawing.Size(109, 23);
            this.B_ObtainNewToken.TabIndex = 3;
            this.B_ObtainNewToken.Text = "Obtain new Token";
            this.B_ObtainNewToken.UseVisualStyleBackColor = true;
            this.B_ObtainNewToken.Click += new System.EventHandler(this.B_ObtainNewToken_Click);
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(301, 62);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 4;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(382, 62);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 5;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // LoginSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 92);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_ObtainNewToken);
            this.Controls.Add(this.CB_ShowPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_Password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginSettings";
            this.Text = "Login Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.CheckBox CB_ShowPassword;
        private System.Windows.Forms.Button B_ObtainNewToken;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
    }
}