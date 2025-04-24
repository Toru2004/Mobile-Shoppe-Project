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
using System.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;

namespace mobileshoppe
{
    public partial class adminHomepage : Form
    {

        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        DataSet ds;
        void autoGenid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))


                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT ISNULL(MAX(CompanyID),0) from tbl_Company", conn);
                    object result = cmd.ExecuteScalar();
                    int i = 0;
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out i))
                    {
                        i++;
                        txtCompID.Text = i.ToString();
                    }
                    else
                    {
                        txtCompID.Text = "";
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving ID: " + ex.Message);
            }

        }
        void autoModId()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT ISNULL(MAX(ModelID),0) from tbl_Model", conn);
                    object result = cmd.ExecuteScalar();
                    int i = 0;
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out i))
                    {
                        i++;
                        txtModID.Text = i.ToString();
                    }
                    else
                    {
                        txtModID.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving ID: " + ex.Message);
            }

        }
        void autoTransId()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))


                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT ISNULL(MAX(TransactionID),0) from tbl_Transaction", conn);
                    object result = cmd.ExecuteScalar();
                    int i = 0;
                    if (result != DBNull.Value && result != null && int.TryParse(result.ToString(), out i))
                    {
                        i++;
                        txtTransID.Text = i.ToString();
                    }
                    else
                    {
                        txtTransID.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving ID: " + ex.Message);
            }

        }
        public adminHomepage()
        {
            InitializeComponent();
        }


        private void adminHomepage_Load(object sender, EventArgs e)
        {
            autoGenid();
            autoModId();
            autoTransId();
            BindingCompanyName();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                int CompanyID = int.Parse(txtCompID.Text);
                string CompanyName = txtCompName.Text;
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    cmd = new SqlCommand("Insert into tbl_Company values(@CompanyID, @CompanyName) ", conn);
                    cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công", "Thông Báo!");
                    autoGenid();
                    txtCompName.Clear();
                    BindingCompanyName();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding record: " + ex.Message);
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int transID = int.Parse(txtTransID.Text);
            //int ModelID = Convert.ToInt32(cboModNo.SelectedValue);
            decimal amount = decimal.Parse(txtAmount.Text); // Chuyển đổi giá trị từ string sang decimal
            int Aquantity = int.Parse(txtQuantity.Text);


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                using (SqlCommand cmdGetModId = new SqlCommand("SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                {
                    cmdGetModId.Parameters.AddWithValue("@ModelNumber", cboModNo.Text.Trim());
                    int ModelID = Convert.ToInt32(cmdGetModId.ExecuteScalar());

                    cmd = new SqlCommand("Insert into tbl_Transaction values(@transID, @ModelID, @Aquantity, GETDATE(), @amount) ", conn);
                    cmd.Parameters.AddWithValue("@transID", transID);
                    cmd.Parameters.AddWithValue("@ModelID", ModelID);
                    cmd.Parameters.AddWithValue("@Aquantity", Aquantity);
                    cmd.Parameters.AddWithValue("@amount", amount);


                    cmd.ExecuteNonQuery();
                    using (SqlCommand cmdmodel = new SqlCommand("update tbl_Model set AvailableQty =  @Aquantity where ModelID = @ModelID ", conn))
                    {
                        cmdmodel.Parameters.AddWithValue("@Aquantity", Aquantity);
                        cmdmodel.Parameters.AddWithValue("@ModelID", ModelID);
                        cmdmodel.ExecuteNonQuery();
                    }
                    MessageBox.Show("Cập nhật thành công", "Thông Báo!");

                    autoTransId();
                    txtQuantity.Clear();
                    txtAmount.Clear();
                }
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
                cboCompNameMod.DataSource = ds.Tables["tbl_Company"];
                cboCompNameMod.DisplayMember = "CompanyName";
                cboCompNameMod.ValueMember = "CompanyID";

                cboCompNameMobile.DataSource = ds.Tables["tbl_Company"];
                cboCompNameMobile.DisplayMember = "CompanyName";
                cboCompNameMobile.ValueMember = "CompanyID";

                cboCompNameUp.DataSource = ds.Tables["tbl_Company"];
                cboCompNameUp.DisplayMember = "CompanyName";
                cboCompNameUp.ValueMember = "CompanyID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding tbl_Company names: " + ex.Message);
            }
        }
        internal void BindingModelNumber()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * from tbl_Model", conn);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "tbl_Model");//khoogn có tbl_Company thì k hiện đâu
                }
                cboModNoMobile.DataSource = ds.Tables["tbl_Model"];
                cboModNoMobile.DisplayMember = "ModelNumber";
                cboModNoMobile.ValueMember = "ModelID";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding tbl_Model names: " + ex.Message);
            }
        }
        private void btnAddMod_Click(object sender, EventArgs e)
        {
            int ModelID = int.Parse(txtModID.Text);
            int CompanyID = Convert.ToInt32(cboCompNameMod.SelectedValue);
            string ModelNumber = txtNum.Text;// tài liệu lúc string lúc int
            int AvailableQty;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("Insert into tbl_Model values(@ModelID, @ModelNumber, @AvailableQty, @CompanyID) ", conn);
                cmd.Parameters.AddWithValue("@ModelID", ModelID);
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                cmd.Parameters.AddWithValue("@AvailableQty", 0);
                cmd.Parameters.AddWithValue("@ModelNumber", ModelNumber);


                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công", "Thông Báo!");

                autoModId();
                txtNum.Clear();
            }
        }



        private void cboCompNameMobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // int CompanyID = Convert.ToInt32(cboCompNameMod.SelectedValue);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNumber FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.CompanyID = tbl_Company.CompanyID WHERE tbl_Company.CompanyName = @CompanyName;", conn);
                cmd.Parameters.AddWithValue("@CompanyName", cboCompNameMobile.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNoMobile.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNumber" vào cboModNoMobile
                        cboModNoMobile.Items.Add(dr["ModelNumber"]);
                    }
                }
            }
        }

        private void cboCompNameUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNumber FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.CompanyID = tbl_Company.CompanyID WHERE tbl_Company.CompanyName = @CompanyName;", conn);
                cmd.Parameters.AddWithValue("@CompanyName", cboCompNameUp.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNo.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNumber" vào cboModNoMobile
                        cboModNo.Items.Add(dr["ModelNumber"]);
                    }
                }
            }
        }

        private void btnAddMobile_Click(object sender, EventArgs e)
        {
            int IMEINO = int.Parse(txtIMEINo.Text);
            //int ModelID = Convert.ToInt32(cboModNo.SelectedValue);
            decimal Price = decimal.Parse(txtPrice.Text); // Chuyển đổi giá trị từ string sang decimal
            DateTime Warranty = dtpWarr.Value;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                using (SqlCommand getModIdCmd = new SqlCommand("SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                {
                    getModIdCmd.Parameters.AddWithValue("@ModelNumber", cboModNoMobile.Text.Trim());
                    int ModelID = Convert.ToInt32(getModIdCmd.ExecuteScalar());

                    cmd = new SqlCommand("Insert into tbl_Mobile values(@IMEINO, @ModelID, 'Not Sold', @Price, @Warranty) ", conn);
                    cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                    cmd.Parameters.AddWithValue("@ModelID", ModelID);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Warranty", Warranty);


                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công", "Thông Báo!");

                    autoTransId();
                    txtIMEINo.Clear();
                    txtPrice.Clear();
                }
            }
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = txtUser.Text;
                string Pwd = txtPass.Text;
                string EmployeeName = txtEmpName.Text;
                string Address = txtAdd.Text;
                string MobileNumber = txtMobleNo.Text;
                string Hint = txtHint.Text;
                string repwd = txtRepass.Text;
                if (Pwd.Equals(repwd))
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                    {
                        cmd = new SqlCommand("Insert into [user] values(@Username, @Pwd, @EmployeeName, @Address, @MobileNumber, @Hint) ", conn);
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Pwd", Pwd);
                        cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                        cmd.Parameters.AddWithValue("@Hint", Hint);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công", "Thông Báo!");

                    }
                }else
                {
                    txtRepass.Text = "";
                    MessageBox.Show("Thêm không thành công", "Thông Báo!");
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding record: " + ex.Message);
            }
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            adminLogin adminLogin = new adminLogin();
            adminLogin.Show();
            this.Close();
        }

        private void lblSearchDay_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtpDay.Value);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                cmd = new SqlCommand("select s1.SalesID, c1.CompanyName, md1.ModelNumber, s1.IMEINO, s1.Price from tbl_Sales s1 inner join tbl_Mobile mb1 on s1.IMEINO = mb1.IMEINO   inner join tbl_Model md1 on mb1.ModelID = md1.ModelID   inner join tbl_Company c1 on  md1.CompanyID = c1.CompanyID where s1.SalesDate = @date", conn);
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Đổ dữ liệu vào DataGridView
                dgSaleReportDay.DataSource = dt;
            }
        }

        private void dtpDay_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void lblSearchDate_Click(object sender, EventArgs e)
        {
            DateTime dateMin = Convert.ToDateTime(dtpDateMin.Value);

            DateTime dateMax = Convert.ToDateTime(dtpDateMax.Value);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                cmd = new SqlCommand("select s1.SalesID, c1.CompanyName, md1.ModelNumber, s1.IMEINO, s1.Price\r\nfrom tbl_Sales s1 inner join tbl_Mobile mb1 on s1.IMEINO = mb1.IMEINO\r\n\t  inner join tbl_Model md1 on mb1.ModelID = md1.ModelID\r\n\t  inner join tbl_Company c1 on  md1.CompanyID = c1.CompanyID\r\nWHERE s1.SalesDate BETWEEN @dateMin AND @dateMax;", conn);
                cmd.Parameters.AddWithValue("@dateMin", dateMin);
                cmd.Parameters.AddWithValue("@dateMax", dateMax);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Đổ dữ liệu vào DataGridView
                dgSaleReportDate.DataSource = dt;
            }
        }
    }
}
