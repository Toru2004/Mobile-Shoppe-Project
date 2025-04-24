namespace mobileshoppe
{
    partial class userLogin
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
            this.link_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.link_forgotpass = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // link_label
            // 
            this.link_label.AutoSize = true;
            this.link_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_label.Location = new System.Drawing.Point(280, 32);
            this.link_label.Name = "link_label";
            this.link_label.Size = new System.Drawing.Size(45, 16);
            this.link_label.TabIndex = 0;
            this.link_label.Text = "Admin";
            this.link_label.Click += new System.EventHandler(this.link_label_Click);
            this.link_label.MouseLeave += new System.EventHandler(this.link_label_MouseLeave);
            this.link_label.MouseHover += new System.EventHandler(this.link_label_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.Location = new System.Drawing.Point(224, 186);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(81, 29);
            this.btn_login.TabIndex = 3;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(179, 86);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(100, 20);
            this.txtUid.TabIndex = 4;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(179, 138);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(100, 20);
            this.txtPwd.TabIndex = 5;
            // 
            // link_forgotpass
            // 
            this.link_forgotpass.AutoSize = true;
            this.link_forgotpass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link_forgotpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_forgotpass.Location = new System.Drawing.Point(212, 240);
            this.link_forgotpass.Name = "link_forgotpass";
            this.link_forgotpass.Size = new System.Drawing.Size(113, 16);
            this.link_forgotpass.TabIndex = 6;
            this.link_forgotpass.Text = "ForgotPassword?";
            this.link_forgotpass.Click += new System.EventHandler(this.link_forgotpass_Click);
            this.link_forgotpass.MouseLeave += new System.EventHandler(this.link_forgotpass_MouseLeave);
            this.link_forgotpass.MouseHover += new System.EventHandler(this.link_forgotpass_MouseHover);
            // 
            // userLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 287);
            this.Controls.Add(this.link_forgotpass);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUid);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.link_label);
            this.Name = "userLogin";
            this.Text = "form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label link_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label link_forgotpass;
    }
}

