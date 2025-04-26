using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace mobileshoppe
{
    public partial class userHomepage : Form
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        SqlDataReader dr;
        public userHomepage()
        {
            InitializeComponent();
            BindingCompanyName();
            BindingStock();
        }
        internal void BindingStock()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd.Parameters.Clear();
                cmd = new SqlCommand("select AvailableQty from tbl_Model  where ModelNumber = @ModelNumber", conn);
                cmd.Parameters.AddWithValue("@ModelNumber", cboModNumVS.Text);
                cmd.ExecuteNonQuery(); 
                int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                txtStock.Text = i.ToString();
            }
        }
        internal void BindingCompanyName()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * from tbl_Company", conn);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "tbl_Company");//khoogn có tbl_Company thì k hiện đâu
                }
                cboCompName.DataSource = ds.Tables["tbl_Company"];
                cboCompName.DisplayMember = "CompanyName";
                cboCompName.ValueMember = "CompanyID";
                cboCompName.SelectedIndex = -1;
                cboCompName.Text = "";

                cboCompNameVS.DataSource = ds.Tables["tbl_Company"];
                cboCompNameVS.DisplayMember = "CompanyName";
                cboCompNameVS.ValueMember = "CompanyID";
                cboCompNameVS.SelectedIndex = -1;
                cboCompNameVS.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding tbl_Company names: " + ex.Message);
            }
        }
        private void cboCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNumber FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.CompanyID = tbl_Company.CompanyID WHERE tbl_Company.CompanyName = @CompanyName;", conn);
                cmd.Parameters.AddWithValue("@CompanyName", cboCompName.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNum.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNumber" vào cboModNoMobile
                        cboModNum.Items.Add(dr["ModelNumber"]);
                    }
                    if (cboModNum.Items.Count > 0)
                    {
                        cboModNum.SelectedIndex = 0;
                    }
                }
            }
            cboModNum.SelectedIndex = -1;
            cboModNum.Text = "";
            cboIMIENum.SelectedIndex = -1;
            cboIMIENum.Text = "";
        }
        internal void BindingIMEI()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand("select * from tbl_Mobile  ", conn);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "tbl_Mobile");//khoogn có tbl_Company thì k hiện đâu
                }
                cboIMIENum.DataSource = ds.Tables["tbl_Mobile"];
                cboIMIENum.DisplayMember = "IMEINO";
                cboIMIENum.ValueMember = "IMEINO";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding tbl_Mobile names: " + ex.Message);
            }
        }

        

        private void cboModNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT IMEINO from tbl_Mobile  inner join tbl_Model on  tbl_Mobile.ModelID = tbl_Model.ModelID WHERE tbl_Model.ModelNumber = @ModelNumber", conn);
                cmd.Parameters.AddWithValue("@ModelNumber", cboModNum.Text);

                dr = cmd.ExecuteReader();
                {
                    cboIMIENum.Items.Clear();

                    while (dr.Read())
                    {
                        cboIMIENum.Items.Add(dr["IMEINO"]);
                    }
                    if (cboIMIENum.Items.Count > 0)
                    {
                        cboIMIENum.SelectedIndex = 0;
                    }
                }
            }
            cboIMIENum.SelectedIndex = -1;
            cboIMIENum.Text = "";
        }
        private void btnSubmitSales_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                return;
            }

            if (!IsValidPhoneNumber(txtMobNum.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ (10 số, bắt đầu bằng 0).");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtaddress.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.");
                return;
            }

            if (!IsValidEmail(txtEmailId.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email hợp lệ.");
                return;
            }

            if (cboCompName.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hãng.");
                return;
            }

            if (cboModNum.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn model.");
                return;
            }

            if (cboIMIENum.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn IMEI.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng kiểm tra giá sản phẩm.");
                return;
            }

            // Nếu tất cả đều OK thì lưu thông tin
            confirmDetails.CustomerName = txtCustName.Text;
            confirmDetails.MobNum = txtMobNum.Text;
            confirmDetails.Address = txtaddress.Text;
            confirmDetails.email = txtEmailId.Text;
            confirmDetails.CompanyName = cboCompName.Text;
            confirmDetails.modnum = cboModNum.Text;
            confirmDetails.IMEI = cboIMIENum.Text;
            confirmDetails.Price = txtPrice.Text;

            confirmDetails objdetails = new confirmDetails();
            objdetails.Show();
            this.Hide();
        }


        private void cboCompNameVS_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNumber FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.CompanyID = tbl_Company.CompanyID WHERE tbl_Company.CompanyName = @CompanyName;", conn);
                cmd.Parameters.AddWithValue("@CompanyName", cboCompNameVS.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNumVS.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNumber" vào cboModNoMobile
                        cboModNumVS.Items.Add(dr["ModelNumber"]);
                    }
                    if (cboModNumVS.Items.Count > 0)
                    {
                        cboModNumVS.SelectedIndex = 0;
                    }

                }
            }
            cboModNumVS.SelectedIndex = -1;
            cboModNumVS.Text = "";
        }

        private void cboModNumVS_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingStock();
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            int IMEI = int.Parse(txtIMEINumSearch.Text);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("select c1.CustomerName, c1.MobileNumber, c1.EmailID, c1.Address from tbl_Sales s1 inner join tbl_Customer c1 on s1.CustomerID = c1.CustomerID where s1.IMEINO = @IMEINO", conn);
                cmd.Parameters.AddWithValue("@IMEINO", IMEI);
                // Sử dụng SqlDataAdapter và DataTable để đổ dữ liệu
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Đổ dữ liệu vào DataGridView
                dgSearchIMIE.DataSource = dt;
            }
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            userLogin userLogin = new userLogin();
            userLogin.Show();
            this.Close();
        }
        private void LoadPrice()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Price FROM tbl_Mobile WHERE IMEINO = @IMEINO", conn);
                cmd.Parameters.AddWithValue("@IMEINO", cboIMIENum.Text);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    txtPrice.Text = Convert.ToDecimal(result).ToString("N0"); // Định dạng kiểu tiền tệ
                }
                else
                {
                    txtPrice.Text = "0";
                }
            }
        }
        private void userHomepage_Load(object sender, EventArgs e)
        {
            BindingCompanyName(); // Load danh sách Company

            if (cboCompName.Items.Count > 0)
            {
                cboCompName.SelectedIndex = 0; // Chọn hãng đầu tiên → tự động kích hoạt load model
            };
            if (cboCompNameVS.Items.Count > 0)
            {
                cboCompNameVS.SelectedIndex = 0; // Chọn hãng đầu tiên → tự động kích hoạt load model
            };
            txtPrice.ReadOnly = true;
            cboCompName.DropDownStyle = ComboBoxStyle.DropDownList;
            cboModNum.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIMIENum.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCompNameVS.DropDownStyle = ComboBoxStyle.DropDownList;
            cboModNumVS.DropDownStyle = ComboBoxStyle.DropDownList;
            txtStock.ReadOnly = true;
        }

        private void cboIMIENum_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPrice();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; // đúng định dạng email
            }
            catch
            {
                return false; // lỗi -> sai định dạng
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            if (phoneNumber.Length != 10)
                return false;

            if (!phoneNumber.All(char.IsDigit)) // kiểm tra tất cả là số
                return false;

            if (!phoneNumber.StartsWith("0")) // kiểm tra bắt đầu bằng 0
                return false;

            return true;
        }

    }
}
