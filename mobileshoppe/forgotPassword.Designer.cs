namespace mobileshoppe
{
    partial class forgotPassword
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpass = new System.Windows.Forms.Label();
            this.lblLoginpage = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.txtHint = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hint";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(147, 145);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Your password is:";
            // 
            // txtpass
            // 
            this.txtpass.AutoSize = true;
            this.txtpass.Location = new System.Drawing.Point(148, 204);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(49, 13);
            this.txtpass.TabIndex = 4;
            this.txtpass.Text = "_ _ _ _ _";
            // 
            // lblLoginpage
            // 
            this.lblLoginpage.AutoSize = true;
            this.lblLoginpage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoginpage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginpage.Location = new System.Drawing.Point(255, 264);
            this.lblLoginpage.Name = "lblLoginpage";
            this.lblLoginpage.Size = new System.Drawing.Size(58, 13);
            this.lblLoginpage.TabIndex = 5;
            this.lblLoginpage.Text = "LoginPage";
            this.lblLoginpage.Click += new System.EventHandler(this.lblLoginpage_Click);
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(131, 32);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(100, 20);
            this.txtUid.TabIndex = 6;
            // 
            // txtHint
            // 
            this.txtHint.Location = new System.Drawing.Point(131, 84);
            this.txtHint.Name = "txtHint";
            this.txtHint.Size = new System.Drawing.Size(100, 20);
            this.txtHint.TabIndex = 7;
            // 
            // forgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 324);
            this.Controls.Add(this.txtHint);
            this.Controls.Add(this.txtUid);
            this.Controls.Add(this.lblLoginpage);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "forgotPassword";
            this.Text = "forgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtpass;
        private System.Windows.Forms.Label lblLoginpage;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.TextBox txtHint;
    }
}