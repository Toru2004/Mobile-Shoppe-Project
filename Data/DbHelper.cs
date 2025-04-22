using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prj_ShoppeMobile.Data
{
    class DbHelper
    {
        private static string connStr = "Data Source=.;Initial Catalog=MobileShoppe;Integrated Security=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
        // Phương thức kiểm tra kết nối
        public static bool TestConnection()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    return true; // Kết nối thành công
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, in ra và trả về false
                    MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
