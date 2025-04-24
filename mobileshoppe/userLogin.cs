using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace mobileshoppe
{
    public partial class userLogin : Form
    {
        public userLogin()
        {
            InitializeComponent();
        }

        private void link_label_Click(object sender, EventArgs e)
        {
            adminLogin objLogin = new adminLogin();
            objLogin.Show();
            this.Hide();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string Username = txtUid.Text;
            string Pwd = txtPwd.Text;
            int check = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select count(*) from tbl_User where Username = @Username and Pwd = @Pwd  ", conn);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Pwd", Pwd);

                conn.Open();
                cmd.ExecuteNonQuery();
                check = (int)cmd.ExecuteScalar();

            }
            if (check == 1)
            {
                userHomepage objuserHome = new userHomepage();
                objuserHome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công", "Thông báo!!!");
                txtUid.Text = "";
                txtPwd.Text = "";
            }

        }

        private void link_forgotpass_Click(object sender, EventArgs e)
        {
            forgotPassword  objforgot = new forgotPassword();
            objforgot.Show();
            this.Hide();
        }

        private void link_label_MouseHover(object sender, EventArgs e)
        {
            link_label.ForeColor = SystemColors.HotTrack;

        }

        private void link_label_MouseLeave(object sender, EventArgs e)
        {
            link_label.ForeColor = SystemColors.ControlText; 
        }

        private void link_forgotpass_MouseHover(object sender, EventArgs e)
        {
            link_forgotpass.ForeColor = SystemColors.HotTrack;
        }

        private void link_forgotpass_MouseLeave(object sender, EventArgs e)
        {
            link_forgotpass.ForeColor = SystemColors.ControlText;

        }
    }
}
