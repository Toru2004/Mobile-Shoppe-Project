using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mobileshoppe
{
    public partial class adminLogin : Form
    {
        public adminLogin()
        {
            InitializeComponent();
        }

        private void adminLogin_Load(object sender, EventArgs e)
        {

        }

        private void linkBack_Click(object sender, EventArgs e)
        {
            userLogin objLogin = new userLogin();
            objLogin.Show();
            this.Hide();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txtUid.Text == "admin" && txtPwd.Text == "admin")
            {
                adminHomepage objAdminHome = new adminHomepage();
                objAdminHome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công", "Thông báo!!!");
                txtPwd.Text = "";
                txtUid.Text = "";
            }
        }

        private void linkBack_MouseHover(object sender, EventArgs e)
        {
            linkBack.ForeColor = SystemColors.HotTrack;
        }

        private void linkBack_MouseLeave(object sender, EventArgs e)
        {
            linkBack.ForeColor = SystemColors.ControlText;
        }
    }
}
