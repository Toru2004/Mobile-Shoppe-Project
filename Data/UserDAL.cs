using Prj_ShoppeMobile.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Prj_ShoppeMobile.Data
{
    class UserDAL
    {
        public User Login(string userName, string pwd)
        {
            User user = null;

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@PWD", pwd);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserName = reader["UserName"].ToString(),
                                PWD = reader["PWD"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                Address = reader["Address"].ToString(),
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Hint = reader["Hint"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}
