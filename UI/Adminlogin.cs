using Prj_ShoppeMobile.Data;
using Prj_ShoppeMobile.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prj_ShoppeMobile.UI
{
    public partial class AdminLogin : Form
    {
        UserDAL userDAL = new UserDAL();
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void linkBack_linkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserLogin objLogin = new UserLogin();
            objLogin.Show();
            this.Hide();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPwd.Text.Trim();

            if (username != "admin")
            {
                MessageBox.Show("Đây không phải tài khoản Admin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User user = userDAL.Login(username, password);

            if (user != null)
            {
                AdminHomepage adminHomepage = new AdminHomepage();
                this.Hide();
                adminHomepage.ShowDialog();
                this.Show();
            }
        }

        private void Adminlogin_Load(object sender, EventArgs e)
        {

        }
    }
}
