using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace mobileshoppe
{
    public partial class confirmDetails : Form
    {
        public static string CustomerName = string.Empty;
        public static string MobNum = string.Empty;
        public static string Address = string.Empty;
        public static string email = string.Empty;
        public static string CompanyName = string.Empty;
        public static string modnum = string.Empty;
        public static string IMEI = string.Empty;
        public static string Price = string.Empty;
        public static string Warranty = string.Empty;

        public confirmDetails()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(CustomerName))
            {
                this.lblcustname.Text = CustomerName;
                //  this.lblmobnum.Text = MobNum;
                // this.lbladdress.Text = Address;
                // this.lblemail.Text = email;
                //this.lblcompname.Text = CompanyName;
                //this.lblmodnum.Text = modnum;
                //this.lblIMEI.Text = IMEI;
                //this.lblprice.Text = Price;
                //this.lblwarr.Text = Warranty;
            }
            if (!string.IsNullOrEmpty(MobNum))
            {
                this.lblmobnum.Text = MobNum;
            }
            if (!string.IsNullOrEmpty(Address))
            {
                this.lbladdress.Text = Address;
            }
            if (!string.IsNullOrEmpty(email))
            {
                this.lblemail.Text = email;
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                this.lblcompname.Text = CompanyName;
            }
            if (!string.IsNullOrEmpty(modnum))
            {
                this.lblmodnum.Text = modnum;
            }
            if (!string.IsNullOrEmpty(IMEI))
            {
                this.lblIMEI.Text = IMEI;
            }
            if (!string.IsNullOrEmpty(Price))
            {
                this.lblprice.Text = Price;
            }
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select Warranty from tbl_Mobile where IMEINO = @IMEINO ", conn))
                {
                    cmd.Parameters.AddWithValue("@IMEINO", IMEI);
                    cmd.ExecuteNonQuery();
                    DateTime dateTime = (DateTime)cmd.ExecuteScalar();
                    Warranty = dateTime.ToString("yyyy-MM-dd");
                    lblwarr.Text = Warranty;
                }
            }
        }

         private int AutoCustomerID()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))


                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CustomerID),0) from tbl_Customer", conn);
                    int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                    i++; 
                    return i;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving ID: " + ex.Message); return 0;
            }

        }
        private int autoSalesID()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))


                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(SalesID),0) from tbl_Sales", conn);
                    int i = (Convert.ToInt32(cmd.ExecuteScalar()));
                    i++;
                    return i;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving ID: " + ex.Message);
                return 0;
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            userHomepage objuser = new userHomepage();
            objuser.Show();
            this.Close();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString()))
            {
                conn.Open();
                int CustomerID = AutoCustomerID();
                int SalesID = autoSalesID();
                using (SqlCommand cmd = new SqlCommand("insert into tbl_Customer values(@CustomerID, @CustomerName, @MobileNumber, @EmailID, @Address)", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                    cmd.Parameters.AddWithValue("@MobileNumber", MobNum);
                    cmd.Parameters.AddWithValue("@EmailID", email);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.ExecuteNonQuery();
                    using (SqlCommand cmdSales = new SqlCommand("insert into tbl_Sales values(@SalesID, @IMEINO, GETDATE(), @Price, @CustomerID)", conn))
                    {
                        cmdSales.Parameters.AddWithValue("@SalesID", SalesID);
                        cmdSales.Parameters.AddWithValue("@IMEINO", IMEI);
                        cmdSales.Parameters.AddWithValue("@Price", Price);
                        cmdSales.Parameters.AddWithValue("@CustomerID", CustomerID);
                        cmdSales.ExecuteNonQuery();
                        using ( SqlCommand cmdmobile = new SqlCommand("update tbl_Mobile set status = 'Sold' where IMEINO = @IMEINO",conn))
                        {
                            cmdmobile.Parameters.AddWithValue("@IMEINO", IMEI);
                            cmdmobile.ExecuteNonQuery();
                            using (SqlCommand cmdGetModId = new SqlCommand("SELECT ModelID FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                            {
                                cmdGetModId.Parameters.AddWithValue("@ModelNumber", lblmodnum.Text);
                                int ModelID = Convert.ToInt32(cmdGetModId.ExecuteScalar());

                                using (SqlCommand cmdGetQty = new SqlCommand("SELECT AvailableQty FROM tbl_Model WHERE ModelNumber = @ModelNumber", conn))
                                {
                                    cmdGetQty.Parameters.AddWithValue("@ModelNumber", lblmodnum.Text);
                                    int Aquantity = Convert.ToInt32(cmdGetQty.ExecuteScalar());
                                    using (SqlCommand cmdmodel = new SqlCommand("update tbl_Model set AvailableQty =  @Aquantity -1 where ModelID = @ModelID ", conn))
                                    {
                                        cmdmodel.Parameters.AddWithValue("@Aquantity", Aquantity);
                                        cmdmodel.Parameters.AddWithValue("@ModelID", ModelID);
                                        cmdmodel.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
                userHomepage objuser = new userHomepage();
                objuser.Show();
                this.Close();
            }
            
        }

        
    }
}
