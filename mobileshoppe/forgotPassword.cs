using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mobileshoppe
{
    public partial class forgotPassword : Form
    {
        public forgotPassword()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUid.Text;
            string hint = txtHint.Text;
            string password;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select pwd from tbl_User where username = @username and hint = @hint  ", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@hint", hint);

                conn.Open();
                cmd.ExecuteNonQuery();
                object result = cmd.ExecuteScalar();

                // Kiểm tra giá trị null trước khi sử dụng
                if (result != null)
                {
                    password = result.ToString();
                    txtpass.Text = password;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mật khẩu", "Thông báo");
                }
            }
        }

        private void lblLoginpage_Click(object sender, EventArgs e)
        {
            userLogin objloggin = new userLogin();
            objloggin.Show();
            this.Hide();
        }
    }
}
