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
                    cmd = new SqlCommand("SELECT ISNULL(MAX(compID),0) from tbl_Company", conn);
                    int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                    i++;
                    txtCompID.Text = i.ToString();


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
                    cmd = new SqlCommand("SELECT ISNULL(MAX(ModID),0) from tbl_Model", conn);
                    int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                    i++;
                    txtModID.Text = i.ToString();


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
                    cmd = new SqlCommand("SELECT ISNULL(MAX(TransID),0) from tbl_Transaction", conn);
                    int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                    i++;
                    txtTransID.Text = i.ToString();


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

                int compID = int.Parse(txtCompID.Text);
                string compName = txtCompName.Text;
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    cmd = new SqlCommand("Insert into tbl_Company values(@compID, @compname) ", conn);
                    cmd.Parameters.AddWithValue("@compID", compID);
                    cmd.Parameters.AddWithValue("@compname", compName);

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
            //int modID = Convert.ToInt32(cboModNo.SelectedValue);
            decimal amount = decimal.Parse(txtAmount.Text); // Chuyển đổi giá trị từ string sang decimal
            int Aquantity = int.Parse(txtQuantity.Text);


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                using (SqlCommand cmdGetModId = new SqlCommand("SELECT ModID FROM tbl_Model WHERE ModelNum = @modNum", conn))
                {
                    cmdGetModId.Parameters.AddWithValue("@modNum", cboModNo.Text.Trim());
                    int modID = Convert.ToInt32(cmdGetModId.ExecuteScalar());

                    cmd = new SqlCommand("Insert into tbl_Transaction values(@transID, @modID, @Aquantity, GETDATE(), @amount) ", conn);
                    cmd.Parameters.AddWithValue("@transID", transID);
                    cmd.Parameters.AddWithValue("@modID", modID);
                    cmd.Parameters.AddWithValue("@Aquantity", Aquantity);
                    cmd.Parameters.AddWithValue("@amount", amount);


                    cmd.ExecuteNonQuery();
                    using (SqlCommand cmdmodel = new SqlCommand("update tbl_Model set AvailableQty =  @Aquantity where ModID = @modID ", conn))
                    {
                        cmdmodel.Parameters.AddWithValue("@Aquantity", Aquantity);
                        cmdmodel.Parameters.AddWithValue("@modID", modID);
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
                cboCompNameMod.DisplayMember = "compName";
                cboCompNameMod.ValueMember = "compID";

                cboCompNameMobile.DataSource = ds.Tables["tbl_Company"];
                cboCompNameMobile.DisplayMember = "compName";
                cboCompNameMobile.ValueMember = "compID";

                cboCompNameUp.DataSource = ds.Tables["tbl_Company"];
                cboCompNameUp.DisplayMember = "compName";
                cboCompNameUp.ValueMember = "compID";
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
                cboModNoMobile.DisplayMember = "modNum";
                cboModNoMobile.ValueMember = "modID";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding tbl_Model names: " + ex.Message);
            }
        }
        private void btnAddMod_Click(object sender, EventArgs e)
        {
            int modID = int.Parse(txtModID.Text);
            int compID = Convert.ToInt32(cboCompNameMod.SelectedValue);
            string modNum = txtNum.Text;// tài liệu lúc string lúc int
            int AvailableQty;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("Insert into tbl_Model values(@modID, @modNum, @AvailableQty, @compID) ", conn);
                cmd.Parameters.AddWithValue("@modID", modID);
                cmd.Parameters.AddWithValue("@compID", compID);
                cmd.Parameters.AddWithValue("@AvailableQty", 0);
                cmd.Parameters.AddWithValue("@modNum", modNum);


                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công", "Thông Báo!");

                autoModId();
                txtNum.Clear();
            }
        }



        private void cboCompNameMobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // int compID = Convert.ToInt32(cboCompNameMod.SelectedValue);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNum FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.compID = tbl_Company.compID WHERE tbl_Company.compName = @compName;", conn);
                cmd.Parameters.AddWithValue("@compName", cboCompNameMobile.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNoMobile.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNum" vào cboModNoMobile
                        cboModNoMobile.Items.Add(dr["ModelNum"]);
                    }
                }
            }
        }

        private void cboCompNameUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNum FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.compID = tbl_Company.compID WHERE tbl_Company.compName = @compName;", conn);
                cmd.Parameters.AddWithValue("@compName", cboCompNameUp.Text);

                dr = cmd.ExecuteReader();
                {
                    // Xóa các mục hiện tại trong cboModNoMobile trước khi thêm mới
                    cboModNo.Items.Clear();

                    while (dr.Read())
                    {
                        // Thêm giá trị từ cột "ModelNum" vào cboModNoMobile
                        cboModNo.Items.Add(dr["ModelNum"]);
                    }
                }
            }
        }

        private void btnAddMobile_Click(object sender, EventArgs e)
        {
            int IMEINO = int.Parse(txtIMEINo.Text);
            //int modID = Convert.ToInt32(cboModNo.SelectedValue);
            decimal price = decimal.Parse(txtPrice.Text); // Chuyển đổi giá trị từ string sang decimal
            DateTime warranty = dtpWarr.Value;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                using (SqlCommand getModIdCmd = new SqlCommand("SELECT ModID FROM tbl_Model WHERE ModelNum = @modNum", conn))
                {
                    getModIdCmd.Parameters.AddWithValue("@modNum", cboModNoMobile.Text.Trim());
                    int modID = Convert.ToInt32(getModIdCmd.ExecuteScalar());

                    cmd = new SqlCommand("Insert into tbl_Mobile values(@IMEINO, @modID, 'Not Sold', @price, @warranty) ", conn);
                    cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                    cmd.Parameters.AddWithValue("@modID", modID);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@warranty", warranty);


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
                string username = txtUser.Text;
                string pwd = txtPass.Text;
                string EmployeeName = txtEmpName.Text;
                string Address = txtAdd.Text;
                string MobileNo = txtMobleNo.Text;
                string Hint = txtHint.Text;
                string repwd = txtRepass.Text;
                if (pwd.Equals(repwd))
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                    {
                        cmd = new SqlCommand("Insert into [user] values(@username, @pwd, @EmployeeName, @Address, @MobileNo, @Hint) ", conn);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@pwd", pwd);
                        cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
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
                cmd = new SqlCommand("select s1.salesID, c1.compName, md1.ModelNum, s1.IMEINO, s1.price from tbl_Sales s1 inner join mobile mb1 on s1.IMEINO = mb1.IMEINO   inner join tbl_Model md1 on mb1.modID = md1.ModID   inner join tbl_Company c1 on  md1.compID = c1.compID where s1.salesDate = @date", conn);
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
                cmd = new SqlCommand("select s1.salesID, c1.compName, md1.ModelNum, s1.IMEINO, s1.price\r\nfrom tbl_Sales s1 inner join mobile mb1 on s1.IMEINO = mb1.IMEINO\r\n\t  inner join tbl_Model md1 on mb1.modID = md1.ModID\r\n\t  inner join tbl_Company c1 on  md1.compID = c1.compID\r\nWHERE s1.salesDate BETWEEN @dateMin AND @dateMax;", conn);
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
