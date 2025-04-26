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
                    cmd = new SqlCommand("SELECT MAX(CompanyID) from tbl_Company", conn);
                    object result = cmd.ExecuteScalar();

                    if (result == DBNull.Value || result == null)
                    {
                        txtCompID.Text = "C000";
                    }
                    else
                    {
                        string lastId = result.ToString();
                        // Trích xuất phần số từ TransactionID (bỏ chữ 'T')
                        if (lastId.StartsWith("C") && lastId.Length > 1)
                        {
                            string numPart = lastId.Substring(1);
                            if (int.TryParse(numPart, out int lastNum))
                            {
                                // Tăng số lên 1 và định dạng thành 3 chữ số
                                lastNum++;
                                txtCompID.Text = "C" + lastNum.ToString("D3");
                            }
                            else
                            {
                                // Nếu không thể parse, tạo ID mặc định
                                txtCompID.Text = "C000";
                            }
                        }
                        else
                        {
                            txtCompID.Text = "C000";
                        }
                    }
                    // Đảm bảo TextBox không cho phép chỉnh sửa
                    txtCompID.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo ID tự động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompID.Text = "C001"; // ID mặc định nếu có lỗi
                txtCompID.ReadOnly = true;
            }

        }
        void autoModId()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT MAX(ModelID) from tbl_Model", conn);
                    object result = cmd.ExecuteScalar();
                  
                    if (result == DBNull.Value || result == null )
                    {
                       
                        txtModID.Text = "M000";
                    }
                    else
                    {
                        string lastId = result.ToString();
                        // Trích xuất phần số từ TransactionID (bỏ chữ 'T')
                        if (lastId.StartsWith("M") && lastId.Length > 1)
                        {
                            string numPart = lastId.Substring(1);
                            if (int.TryParse(numPart, out int lastNum))
                            {
                                // Tăng số lên 1 và định dạng thành 3 chữ số
                                lastNum++;
                                txtModID.Text = "M" + lastNum.ToString("D3");
                            }
                            else
                            {
                                // Nếu không thể parse, tạo ID mặc định
                                txtModID.Text = "M000";
                            }
                        }
                        else
                        {
                            txtModID.Text = "M000";
                        }
                    }
                    // Đảm bảo TextBox không cho phép chỉnh sửa
                    txtModID.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo ID tự động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtModID.Text = "M001"; // ID mặc định nếu có lỗi
                txtModID.ReadOnly = true;
            }

        }
        void autoTransId()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(TransactionID) FROM tbl_Transaction", conn);
                    object result = cmd.ExecuteScalar();

                    if (result == DBNull.Value || result == null)
                    {
                        // Nếu bảng trống, bắt đầu từ T001
                        txtTransID.Text = "T000";
                    } 
                    else
                    {
                        string lastId = result.ToString();
                        // Trích xuất phần số từ TransactionID (bỏ chữ 'T')
                        if (lastId.StartsWith("T") && lastId.Length > 1)
                        {
                            string numPart = lastId.Substring(1);
                            if (int.TryParse(numPart, out int lastNum))
                            {
                                // Tăng số lên 1 và định dạng thành 3 chữ số
                                lastNum++;
                                txtTransID.Text = "T" + lastNum.ToString("D3");
                            }
                            else
                            {
                                // Nếu không thể parse, tạo ID mặc định
                                txtTransID.Text = "T000";
                            }
                        }
                        else
                        {
                            txtTransID.Text = "T000";
                        }
                    }

                    txtTransID.ReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo ID tự động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTransID.Text = "T000"; // ID mặc định nếu có lỗi
                txtTransID.ReadOnly = true;
            }

        }
        void autoIMEINo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(IMEINO) FROM tbl_Mobile", conn);
                    object result = cmd.ExecuteScalar();

                    if (result == DBNull.Value || result == null)
                    {
                        // Nếu bảng trống, bắt đầu từ I000001
                        txtIMEINo.Text = "0";
                    }
                    else
                    {
                        string IMEINoString = result.ToString();

                        // Trích xuất phần số từ IMEINO (bỏ chữ 'I')
                        if (IMEINoString.Length > 1)
                        {
                            if (int.TryParse(IMEINoString, out int IMEINoInt))
                            {
                                IMEINoInt++;
                                txtIMEINo.Text = IMEINoInt.ToString(); // D6: 6 chữ số, ví dụ I000001
                            }
                            else
                            {
                                txtIMEINo.Text = "0";
                            }
                        }
                        else
                        {
                            txtIMEINo.Text = "0";
                        }
                    }

                    txtIMEINo.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo IMEINO tự động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIMEINo.Text = "0"; // ID mặc định nếu có lỗi
                txtIMEINo.ReadOnly = true;
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
            autoIMEINo();
            BindingCompanyName();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string CompanyID = txtCompID.Text;
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
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Lỗi trùng khóa chính hoặc khóa duy nhất
                {
                    MessageBox.Show("Lỗi: Mã công ty đã tồn tại!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string transID = txtTransID.Text.Trim();  // ID tự động tạo từ hàm autoTransId
            string quantity = txtQuantity.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    // Bắt đầu giao dịch
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Lấy ModelID
                            using (SqlCommand cmdGetModId = new SqlCommand(
                                "SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn, transaction))
                            {
                                cmdGetModId.Parameters.AddWithValue("@ModelNumber", cboModNo.Text.Trim());
                                object result = cmdGetModId.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("Không tìm thấy Model.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                string ModelID = result.ToString();

                                // Thêm giao dịch vào bảng tbl_Transaction
                                using (SqlCommand cmd = new SqlCommand(
                                    "INSERT INTO tbl_Transaction (TransactionID, ModelId, Quantity, Date) " +
                                    "VALUES(@transID, @ModelID, @quantity, GETDATE())", conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@transID", transID);
                                    cmd.Parameters.AddWithValue("@ModelID", ModelID);
                                    cmd.Parameters.AddWithValue("@quantity", quantity);
                                    cmd.ExecuteNonQuery();
                                }

                                // Cập nhật số lượng trong bảng tbl_Model
                                using (SqlCommand cmdmodel = new SqlCommand(
                                    "UPDATE tbl_Model SET AvailableQty = AvailableQty + @quantity " +
                                    "WHERE ModelID = @ModelID", conn, transaction))
                                {
                                    cmdmodel.Parameters.AddWithValue("@quantity", Convert.ToInt32(quantity));
                                    cmdmodel.Parameters.AddWithValue("@ModelID", ModelID);
                                    cmdmodel.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Cập nhật thành công", "Thông Báo!");
                                autoTransId();  // Tạo lại ID tự động cho lần tiếp theo
                                txtQuantity.Clear();
                               
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã giao dịch đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    autoTransId();  // Tạo ID mới nếu xảy ra lỗi trùng ID
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string ModelID = txtModID.Text.Trim();
            string CompanyID = cboCompNameMod.SelectedValue.ToString().Trim();
            string ModelNumber = txtNum.Text.Trim();
            int AvailableQty = 0;

            if (string.IsNullOrWhiteSpace(ModelID) || string.IsNullOrWhiteSpace(ModelNumber))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    cmd = new SqlCommand(
                        "INSERT INTO tbl_Model (ModelID, ModelNumber, AvailableQty, CompanyId) " +
                        "VALUES (@ModelID, @ModelNumber, @AvailableQty, @CompanyID)", conn);

                    cmd.Parameters.AddWithValue("@ModelID", ModelID);
                    cmd.Parameters.AddWithValue("@ModelNumber", ModelNumber);
                    cmd.Parameters.AddWithValue("@AvailableQty", AvailableQty);
                    cmd.Parameters.AddWithValue("@CompanyID", CompanyID);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm thành công", "Thông Báo!");

                    autoModId();
                    txtNum.Clear();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Lỗi: Mã Model đã tồn tại!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!int.TryParse(txtIMEINo.Text.Trim(), out int IMEINO))
            {
                MessageBox.Show("IMEI không hợp lệ. Vui lòng nhập số nguyên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal Price))
            {
                MessageBox.Show("Giá không hợp lệ. Vui lòng nhập số.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime Warranty = dtpWarr.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();

                    using (SqlCommand getModIdCmd = new SqlCommand("SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                    {
                        getModIdCmd.Parameters.AddWithValue("@ModelNumber", cboModNoMobile.Text.Trim());
                        string ModelID = getModIdCmd.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(ModelID))
                        {
                            MessageBox.Show("Không tìm thấy Model tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Mobile (IMEINO, ModelID, Status, Price, Warranty) VALUES (@IMEINO, @ModelID, 'Not Sold', @Price, @Warranty)", conn);
                        cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                        cmd.Parameters.AddWithValue("@ModelID", ModelID);
                        cmd.Parameters.AddWithValue("@Price", Price);
                        cmd.Parameters.AddWithValue("@Warranty", Warranty);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật thành công", "Thông Báo!");

                        autoIMEINo();
                        txtIMEINo.Clear();
                        txtPrice.Clear();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("IMEI đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        cmd = new SqlCommand("Insert into tbl_User values(@Username, @Pwd, @EmployeeName, @Address, @MobileNumber, @Hint) ", conn);
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
