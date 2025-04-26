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

                    // Lấy IMEI lớn nhất hiện có trong database
                    cmd = new SqlCommand("SELECT MAX(CAST(IMEINO AS BIGINT)) FROM tbl_Mobile WHERE ISNUMERIC(IMEINO) = 1", conn);
                    object result = cmd.ExecuteScalar();

                    long newImei = 10000000; // Giá trị khởi tạo

                    if (result != null && result != DBNull.Value && long.TryParse(result.ToString(), out long lastImei))
                    {
                        newImei = lastImei + 1; // Tăng lên 1 so với IMEI lớn nhất
                    }

                    // Đảm bảo số IMEI có ít hơn 9 chữ số
                    if (newImei > 99999999)
                    {
                        newImei = 10000000; // Reset nếu vượt quá
                    }

                    // Kiểm tra xem IMEI mới có trùng không (phòng trường hợp có record mới được thêm vào)
                    int attempts = 0;
                    while (attempts < 10) // Giới hạn số lần thử
                    {
                        cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_Mobile WHERE IMEINO = @IMEI", conn);
                        cmd.Parameters.AddWithValue("@IMEI", newImei.ToString());
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 0) break; // Nếu không trùng thì dừng

                        newImei++; // Nếu trùng thì tăng lên 1
                        attempts++;
                    }

                    txtIMEINo.Text = newImei.ToString();
                    txtIMEINo.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo IMEINO tự động: " + ex.Message,
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                // Fallback: tạo số ngẫu nhiên nếu có lỗi
                Random rand = new Random();
                txtIMEINo.Text = rand.Next(10000000, 99999999).ToString();
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
            LoadWarrantyDates();
            BindingCompanyName();
            //BindingModelNumber();
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
                                autoIMEINo();
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
                    da.Fill(ds, "tbl_Company");//khong có tbl_Company thì ko hiện
                }
                cboCompNameMod.DataSource = ds.Tables["tbl_Company"];
                cboCompNameMod.DisplayMember = "CompanyName";
                cboCompNameMod.ValueMember = "CompanyID";
                cboCompNameMod.SelectedIndex = -1;  // Reset không chọn gì hết
                cboCompNameMod.Text = "";

                cboCompNameMobile.DataSource = ds.Tables["tbl_Company"];
                cboCompNameMobile.DisplayMember = "CompanyName";
                cboCompNameMobile.ValueMember = "CompanyID";
                cboCompNameMobile.SelectedIndex = -1;  // Reset không chọn gì hết
                cboCompNameMobile.Text = "";

                cboCompNameUp.DataSource = ds.Tables["tbl_Company"];
                cboCompNameUp.DisplayMember = "CompanyName";
                cboCompNameUp.ValueMember = "CompanyID";
                cboCompNameUp.SelectedIndex = -1;  // Reset không chọn gì hết
                cboCompNameUp.Text = "";

                cboCompNamePrice.DataSource = ds.Tables["tbl_Company"];
                cboCompNamePrice.DisplayMember = "CompanyName";
                cboCompNamePrice.ValueMember = "CompanyID";
                cboCompNamePrice.SelectedIndex = -1;  // Reset không chọn gì hết
                cboCompNamePrice.Text = "";
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

                cboModPrice.DataSource = ds.Tables["tbl_Model"];
                cboModPrice.DisplayMember = "ModelNumber";
                cboModPrice.ValueMember = "ModelID";

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
            try
            {
                if (cboCompNameMobile.SelectedItem == null) return;

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();
                    string query = @"SELECT m.ModelNumber 
                            FROM tbl_Model m
                            INNER JOIN tbl_Company c ON m.CompanyID = c.CompanyID
                            WHERE c.CompanyName = @CompanyName";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CompanyName", cboCompNameMobile.Text);

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    cboModNoMobile.DataSource = dt;
                    cboModNoMobile.DisplayMember = "ModelNumber";
                    cboModNoMobile.ValueMember = "ModelNumber";

                    // Thêm dòng này để làm mới combobox
                    //cboModNoMobile.Refresh();
                    cboModNoMobile.SelectedIndex = -1;  // Reset không chọn gì hết
                    cboModNoMobile.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải Model: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    cboModNo.SelectedIndex = -1;  // Reset không chọn gì hết
                    cboModNo.Text = "";

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
            // 1. VALIDATE INPUTS
            string imei = txtIMEINo.Text.Trim();

            if (string.IsNullOrEmpty(imei) || !imei.All(char.IsDigit))
            {
                MessageBox.Show("IMEI phải là số và không được trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá phải là số dương", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. PROCESS DATA
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();

                    // 2.1. Get ModelID
                    string modelId;
                    using (var cmd = new SqlCommand(
                        "SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModelNumber", cboModNoMobile.Text.Trim());
                        modelId = cmd.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(modelId))
                        {
                            MessageBox.Show("Không tìm thấy Model", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 2.2. Check IMEI exists
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM tbl_Mobile WHERE IMEINO = @IMEI", conn))
                    {
                        cmd.Parameters.AddWithValue("@IMEI", imei);
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("IMEI đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 2.3. Insert new device
                    using (var cmd = new SqlCommand(
                        "INSERT INTO tbl_Mobile (IMEINO, ModelID, Status, Price, Warranty) " +
                        "VALUES (@IMEI, @ModelID, 'Not Sold', @Price, @Warranty)", conn))
                    {
                        cmd.Parameters.AddWithValue("@IMEI", imei);
                        cmd.Parameters.AddWithValue("@ModelID", modelId);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Warranty", Convert.ToInt32(cboWarrMobile.SelectedValue));

                        cmd.ExecuteNonQuery();
                    }

                    // 3. POST-PROCESSING
                    MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form và tạo IMEI mới
                    txtPrice.Clear();
                    GenerateNewUniqueIMEI(conn); // Truyền connection đang mở để tối ưu

                    // Focus vào ô giá để nhập tiếp
                    txtPrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateNewUniqueIMEI(SqlConnection conn)
        {
            try
            {
                // Lấy IMEI lớn nhất hiện có
                using (var cmd = new SqlCommand(
                    "SELECT MAX(CAST(IMEINO AS BIGINT)) FROM tbl_Mobile WHERE ISNUMERIC(IMEINO) = 1", conn))
                {
                    long newImei = 10000000; // Giá trị mặc định

                    if (long.TryParse(cmd.ExecuteScalar()?.ToString(), out long maxImei))
                        newImei = maxImei + 1;

                    // Đảm bảo 8 chữ số
                    if (newImei > 99999999) newImei = 10000000;

                    // Kiểm tra trùng lặp (phòng trường hợp có transaction khác)
                    int attempts = 0;
                    while (attempts < 5)
                    {
                        using (var checkCmd = new SqlCommand(
                            "SELECT COUNT(*) FROM tbl_Mobile WHERE IMEINO = @IMEI", conn))
                        {
                            checkCmd.Parameters.AddWithValue("@IMEI", newImei.ToString());
                            if ((int)checkCmd.ExecuteScalar() == 0) break;
                        }
                        newImei++;
                        attempts++;
                    }

                    txtIMEINo.Text = newImei.ToString();
                }
            }
            catch
            {
                // Fallback: tạo ngẫu nhiên nếu có lỗi
                txtIMEINo.Text = new Random().Next(10000000, 99999999).ToString();
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

        public class ComboItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        // Hàm để tự động load dữ liệu vào ComboBox
        void LoadWarrantyDates()
        {
            List<ComboItem> warrDates = new List<ComboItem>
            {
                new ComboItem { Text = "One Year", Value = 1 },
                new ComboItem { Text = "Two Years", Value = 2 },
                new ComboItem { Text = "Three Years", Value = 3 },
                new ComboItem { Text = "Four Years", Value = 4 }
            };

            cboWarrMobile.DataSource = warrDates;
            cboWarrMobile.DisplayMember = "Text";   // Hiển thị Text
            cboWarrMobile.ValueMember = "Value";    // Lấy Value
        }

        private void cboModPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có model nào được chọn không
                if (cboModPrice.SelectedItem == null || string.IsNullOrEmpty(cboModPrice.Text))
                {
                    cboIMEINo.DataSource = null;
                    cboIMEINo.Items.Clear();
                    return;
                }

                string modelNumber = cboModPrice.Text;

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();

                    // Truy vấn lấy IMEINO theo ModelNumber và trạng thái hợp lệ
                    string query = @"SELECT m.IMEINO 
                            FROM tbl_Mobile m
                            INNER JOIN tbl_Model md ON m.ModelID = md.ModelID
                            WHERE md.ModelNumber = @ModelNumber 
                            AND m.Status IN ('Not Sold', 'In Stock')";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ModelNumber", modelNumber);

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // Cập nhật DataSource cho combobox IMEI
                    cboIMEINo.DataSource = dt;
                    cboIMEINo.DisplayMember = "IMEINO";
                    cboIMEINo.ValueMember = "IMEINO";
                    cboIMEINo.SelectedIndex = -1;
                    cboIMEINo.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách IMEI: " + ex.Message,
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void cboCompNamePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                cmd = new SqlCommand("SELECT tbl_Model.ModelNumber FROM tbl_Model INNER JOIN tbl_Company ON tbl_Model.CompanyID = tbl_Company.CompanyID WHERE tbl_Company.CompanyName = @CompanyName;", conn);
                cmd.Parameters.AddWithValue("@CompanyName", cboCompNamePrice.Text);

                dr = cmd.ExecuteReader();
                {
                    cboModPrice.Items.Clear();

                    while (dr.Read())
                    {
                        cboModPrice.Items.Add(dr["ModelNumber"]);
                    }
                }
            }
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            // Kiểm tra các điều kiện trước khi cập nhật
            if (cboIMEINo.SelectedItem == null || string.IsNullOrEmpty(cboIMEINo.Text))
            {
                MessageBox.Show("Vui lòng chọn IMEI Number", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtPriceUpdate.Text) || !decimal.TryParse(txtPriceUpdate.Text, out decimal price))
            {
                MessageBox.Show("Giá tiền không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
                {
                    conn.Open();

                    // Câu lệnh SQL cập nhật giá dựa trên IMEI Number
                    string query = @"UPDATE tbl_Mobile 
                            SET Price = @Price 
                            WHERE IMEINO = @IMEINO";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@IMEINO", cboIMEINo.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Có thể thêm code làm mới dữ liệu nếu cần
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy IMEI Number tương ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật giá: " + ex.Message,
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
    }
}
