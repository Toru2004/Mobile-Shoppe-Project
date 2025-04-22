using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prj_ShoppeMobile.UI
{
    public partial class UserHomepage : Form
    {
        public UserHomepage()
        {
            InitializeComponent();
            BindCompanyNames();

            cbCompanyName.SelectedIndexChanged += (s, e) => BindModelNumbers();
            cbModelNumber.SelectedIndexChanged += (s, e) => BindIMEINumbers();
            btnSubmit.Click += (s, e) => SubmitSale();

            // VIEW STOCK
            InitViewStock();
            lnkSearch.Click += (s, e) => SearchCustomerByIMEI();
        }

        string connectionString = "Data Source=PHUONGLAPTOP\\MSSQLSERVER04;Initial Catalog=MobileShoppe;User ID=sa;Password=";

        // 1. Load danh sách hãng
        void BindCompanyNames()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ComId, CName FROM tbl_Company";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbCompanyName.DataSource = dt;
                cbCompanyName.DisplayMember = "CName";
                cbCompanyName.ValueMember = "ComId";

                if (dt.Rows.Count > 0)
                {
                    cbCompanyName.SelectedIndex = 0; // Chọn mục đầu tiên
                    BindModelNumbers(); // Gọi thủ công thay vì rely vào SelectedIndexChanged
                }
            }
        }


        // 2. Load model theo hãng
        void BindModelNumbers()
        {
            if (cbCompanyName.SelectedValue == null || cbCompanyName.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string comId = cbCompanyName.SelectedValue.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ModelId, ModelNum FROM tbl_Model WHERE ComId = @comId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@comId", comId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbModelNumber.DataSource = dt;
                cbModelNumber.DisplayMember = "ModelNum";
                cbModelNumber.ValueMember = "ModelId";

                if (dt.Rows.Count > 0)
                {
                    cbModelNumber.SelectedIndex = 0;
                    BindIMEINumbers();
                }
            }
        }

        // 3. Load IMEI chưa bán
        void BindIMEINumbers()
        {
            if (cbModelNumber.SelectedValue == null || cbModelNumber.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string modelId = cbModelNumber.SelectedValue.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IMEINO FROM tbl_Mobile WHERE ModelId = @modelId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@modelId", modelId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbIMEINumber.DataSource = dt;
                cbIMEINumber.DisplayMember = "IMEINO";
                cbIMEINumber.ValueMember = "IMEINO";
            }
        }

        // 4. Xử lý bán hàng
        void SubmitSale()
        {
            string warranty = GetWarrantyByIMEI(cbIMEINumber.Text);

            ConformDetails conformForm = new ConformDetails(
                txtCustomerName.Text,
                txtMobileNumber.Text,
                txtAddress.Text,
                txtEmail.Text,
                cbCompanyName.Text,
                cbModelNumber.Text,
                cbIMEINumber.Text,
                txtPricePerPiece.Text,
                warranty
            );

            conformForm.ShowDialog();

        }
        string GetWarrantyByIMEI(string imei)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Warranty FROM tbl_Mobile WHERE IMEINO = @imei", conn);
                cmd.Parameters.AddWithValue("@imei", imei);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDateTime(result).ToString("dd/MM/yyyy") : "Không có";
            }
        }


        private void UserHomepage_Load(object sender, EventArgs e)
        {
            cbModelNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIMEINumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSclCompanyName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCompanyName.DropDownStyle = ComboBoxStyle.DropDownList;
            cdSclModelNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvSearchResult.AutoGenerateColumns = false;
            dgvSearchResult.ReadOnly = true;


        }

        void InitViewStock()
        {
            txtStockAvailable.ReadOnly = true;

            BindCompanyNames_ViewStock();
            cbSclCompanyName.SelectedIndexChanged += (s, e) => BindModelNumbers_ViewStock();
            cdSclModelNumber.SelectedIndexChanged += (s, e) => ShowStock_ViewStock();

            if (cbSclCompanyName.Items.Count > 0)
            {
                cbSclCompanyName.SelectedIndex = 0;
                BindModelNumbers_ViewStock();
            }
        }
        void BindCompanyNames_ViewStock()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ComId, CName FROM tbl_Company";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbSclCompanyName.DataSource = dt;
                cbSclCompanyName.DisplayMember = "CName";
                cbSclCompanyName.ValueMember = "ComId";
            }
        }

        void BindModelNumbers_ViewStock()
        {
            if (cbSclCompanyName.SelectedValue == null || cbSclCompanyName.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string comId = cbSclCompanyName.SelectedValue.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ModelId, ModelNum FROM tbl_Model WHERE ComId = @comId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@comId", comId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cdSclModelNumber.DataSource = dt;
                cdSclModelNumber.DisplayMember = "ModelNum";
                cdSclModelNumber.ValueMember = "ModelId";

                if (dt.Rows.Count > 0)
                {
                    cdSclModelNumber.SelectedIndex = 0;
                    ShowStock_ViewStock(); // ✅ đúng logic
                }
                else
                {
                    txtStockAvailable.Text = "0";
                }
            }
        }


        void ShowStock_ViewStock()
        {
            if (cdSclModelNumber.SelectedValue == null || cdSclModelNumber.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string modelId = cdSclModelNumber.SelectedValue.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AvailableQty FROM tbl_Model WHERE ModelId = @modelId", conn);
                cmd.Parameters.AddWithValue("@modelId", modelId);
                object result = cmd.ExecuteScalar();
                txtStockAvailable.Text = result != null ? result.ToString() : "0";
            }
        }

        void SearchCustomerByIMEI()
        {
            string imei = txtEntIMEINumber.Text.Trim();

            if (string.IsNullOrEmpty(imei))
            {
                MessageBox.Show("Vui lòng nhập IMEI!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.CustName AS [Customer Name],
                        c.MobileNumber AS [Mobile Number],
                        c.EmailId AS [Email ID],
                        c.Address AS [Address]
                    FROM tbl_Sales s
                    JOIN tbl_Customer c ON s.CusId = c.CusId
                    WHERE s.IMEINO = @imei";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@imei", imei);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvSearchResult.DataSource = dt;
                    dgvSearchResult.Columns[0].DataPropertyName = "Customer Name";
                    dgvSearchResult.Columns[1].DataPropertyName = "Mobile Number";
                    dgvSearchResult.Columns[2].DataPropertyName = "Email ID";
                    dgvSearchResult.Columns[3].DataPropertyName = "Address";
                }
                else
                {
                    dgvSearchResult.DataSource = null;
                    MessageBox.Show("Không tìm thấy thông tin khách hàng với IMEI này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
