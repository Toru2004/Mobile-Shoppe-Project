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
using System.Threading.Tasks;
using System.Windows.Forms;

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
                cmd = new SqlCommand("select AvailableQty from tbl_Model  where ModelNum = @ModelNum", conn);
                cmd.Parameters.AddWithValue("@ModelNum", cboModNumVS.Text);
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
                cboCompName.DisplayMember = "compName";
                cboCompName.ValueMember = "compID";

                cboCompNameVS.DataSource = ds.Tables["tbl_Company"];
                cboCompNameVS.DisplayMember = "compName";
                cboCompNameVS.ValueMember = "compID";
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
                cmd = new SqlCommand("SELECT tbl_Model.ModelNum FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.compID = tbl_Company.compID WHERE tbl_Company.compName = @compName;", conn);
                cmd.Parameters.AddWithValue("@compName", cboCompName.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNum.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNum" vào cboModNoMobile
                        cboModNum.Items.Add(dr["ModelNum"]);
                    }
                }
            }
        }
        internal void BindingIMEI()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            //    {
            //        conn.Open();
            //        cmd = new SqlCommand("select * from mobile  ", conn); 
            //        da = new SqlDataAdapter(cmd);
            //        ds = new DataSet();
            //        da.Fill(ds, "mobile");//khoogn có tbl_Company thì k hiện đâu
            //    }
            //    cboIMIENum.DataSource = ds.Tables["mobile"];
            //    cboIMIENum.DisplayMember = "IMEINO";
            //    cboIMIENum.ValueMember = "IMEINO";

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error binding mobile names: " + ex.Message);
            //}
        }

        

        private void cboModNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT IMEINO from mobile  inner join tbl_Model on  mobile.modID = tbl_Model.ModID WHERE tbl_Model.ModelNum = @ModelNum", conn);
                cmd.Parameters.AddWithValue("@ModelNum", cboModNum.Text);

                dr = cmd.ExecuteReader();
                {
                    cboIMIENum.Items.Clear();

                    while (dr.Read())
                    {
                        cboIMIENum.Items.Add(dr["IMEINO"]);
                    }
                }
            }
        }
        private void btnSubmitSales_Click(object sender, EventArgs e)
        {
            confirmDetails.CustName = txtCustName.Text;
            confirmDetails.MobNum = txtMobNum.Text;
            confirmDetails.Address = txtaddress.Text;
            confirmDetails.email = txtEmailId.Text;
            confirmDetails.compname = cboCompName.Text;
            confirmDetails.modnum = cboModNum.Text;
            confirmDetails.IMEI = cboIMIENum.Text;
            confirmDetails.Price = txtPrice.Text;
            //confirmDetails.CustName = Warranty.Text;
            confirmDetails objdetails = new confirmDetails();
            objdetails.Show() ;
            this.Hide();




        }
         
        private void cboCompNameVS_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNum FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.compID = tbl_Company.compID WHERE tbl_Company.compName = @compName;", conn);
                cmd.Parameters.AddWithValue("@compName", cboCompNameVS.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNumVS.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNum" vào cboModNoMobile
                        cboModNumVS.Items.Add(dr["ModelNum"]);
                    }
                   
                }
            }
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
                cmd = new SqlCommand("select c1.custName, c1.MobileNo, c1.mailId, c1.Address from tbl_Sales s1 inner join tbl_Customer c1 on s1.custID = c1.custID where s1.IMEINO = @IMEINO", conn);
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
    }
}
