using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prj_ShoppeMobile.UI
{
    public partial class ConformDetails : Form
    {
        string connectionString = "Data Source=PHUONGLAPTOP\\MSSQLSERVER04;Initial Catalog=MobileShoppe;User ID=sa;Password=";

        string customerName, mobile, address, email, company, model, imei, price, warranty;


        public ConformDetails(string name, string mobile, string address, string email,
                      string company, string model, string imei, string price, string warranty)
        {
            InitializeComponent();

            // Lưu vào biến
            this.customerName = name;
            this.mobile = mobile;
            this.address = address;
            this.email = email;
            this.company = company;
            this.model = model;
            this.imei = imei;
            this.price = price;
            this.warranty = warranty;

            // Gán label
            lbCusName.Text =  customerName;
            lbMobile.Text =  mobile;
            lbAddress.Text =  address;
            lbEmail.Text =  email;
            lbComName.Text = company;
            lbModel.Text = model;
            lbIMEI.Text = imei;
            lbPrice.Text =  price;
            lbWarranty.Text = "Warranty: " + warranty;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string custId = GenerateNewCustomerId(conn);
                string slsId = GenerateNewSalesId(conn);

                // 1. Thêm khách hàng
                SqlCommand cmd1 = new SqlCommand(@"
                    INSERT INTO tbl_Customer (CusId, CustName, MobileNumber, EmailId, Address)
                    VALUES (@id, @name, @mobile, @email, @address)", conn);
                cmd1.Parameters.AddWithValue("@id", custId);
                cmd1.Parameters.AddWithValue("@name", customerName);
                cmd1.Parameters.AddWithValue("@mobile", mobile);
                cmd1.Parameters.AddWithValue("@email", email);
                cmd1.Parameters.AddWithValue("@address", address);
                cmd1.ExecuteNonQuery();

                // 2. Thêm hóa đơn bán
                SqlCommand cmd2 = new SqlCommand(@"
                    INSERT INTO tbl_Sales (SlsId, IMEINO, PurchageDate, Price, CusId)
                    VALUES (@slsid, @imei, GETDATE(), @price, @cusid)", conn);
                cmd2.Parameters.AddWithValue("@slsid", slsId);
                cmd2.Parameters.AddWithValue("@imei", imei);
                cmd2.Parameters.AddWithValue("@price", Convert.ToDecimal(price));
                cmd2.Parameters.AddWithValue("@cusid", custId);
                cmd2.ExecuteNonQuery();

                // 3. Cập nhật trạng thái IMEI
                SqlCommand cmd3 = new SqlCommand("UPDATE tbl_Mobile SET Status = 'Sold' WHERE IMEINO = @imei", conn);
                cmd3.Parameters.AddWithValue("@imei", imei);
                cmd3.ExecuteNonQuery();

                // 4. Trừ số lượng tồn kho
                SqlCommand cmd4 = new SqlCommand(@"
                    UPDATE tbl_Model SET AvailableQty = AvailableQty - 1
                    WHERE ModelId = (SELECT ModelId FROM tbl_Mobile WHERE IMEINO = @imei)", conn);
                cmd4.Parameters.AddWithValue("@imei", imei);
                cmd4.ExecuteNonQuery();

                MessageBox.Show("Đã lưu dữ liệu thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Không làm gì
        }

        string GenerateNewCustomerId(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 CusId FROM tbl_Customer ORDER BY CusId DESC", conn);
            var last = cmd.ExecuteScalar()?.ToString() ?? "CU000";
            int next = int.Parse(last.Substring(2)) + 1;
            return "CU" + next.ToString("D3");
        }

        string GenerateNewSalesId(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 SlsId FROM tbl_Sales ORDER BY SlsId DESC", conn);
            var last = cmd.ExecuteScalar()?.ToString() ?? "S000";
            int next = int.Parse(last.Substring(1)) + 1;
            return "S" + next.ToString("D3");
        }
    }
}
