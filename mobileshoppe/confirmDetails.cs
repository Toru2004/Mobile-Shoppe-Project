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
        public static string CustName = string.Empty;
        public static string MobNum = string.Empty;
        public static string Address = string.Empty;
        public static string email = string.Empty;
        public static string compname = string.Empty;
        public static string modnum = string.Empty;
        public static string IMEI = string.Empty;
        public static string Price = string.Empty;
        public static string Warranty = string.Empty;

        public confirmDetails()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(CustName))
            {
                this.lblcustname.Text = CustName;
                //  this.lblmobnum.Text = MobNum;
                // this.lbladdress.Text = Address;
                // this.lblemail.Text = email;
                //this.lblcompname.Text = compname;
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
            if (!string.IsNullOrEmpty(compname))
            {
                this.lblcompname.Text = compname;
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
                using (SqlCommand cmd = new SqlCommand("Select warranty from tbl_Mobile where IMEINO = @IMEINO ", conn))
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
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(custID),0) from tbl_Customer", conn);
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
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(salesID),0) from tbl_Sales", conn);
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
                int custID = AutoCustomerID();
                int salesID = autoSalesID();
                using (SqlCommand cmd = new SqlCommand("insert into tbl_Customer values(@custID, @custName, @MobileNo, @mailId, @Address)", conn))
                {
                    cmd.Parameters.AddWithValue("@custID", custID);
                    cmd.Parameters.AddWithValue("@custName", CustName);
                    cmd.Parameters.AddWithValue("@MobileNo", MobNum);
                    cmd.Parameters.AddWithValue("@mailId", email);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.ExecuteNonQuery();
                    using (SqlCommand cmdSales = new SqlCommand("insert into tbl_Sales values(@salesID, @IMEINO, GETDATE(), @price, @custID)", conn))
                    {
                        cmdSales.Parameters.AddWithValue("@salesID", salesID);
                        cmdSales.Parameters.AddWithValue("@IMEINO", IMEI);
                        cmdSales.Parameters.AddWithValue("@price", Price);
                        cmdSales.Parameters.AddWithValue("@custID", custID);
                        cmdSales.ExecuteNonQuery();
                        using ( SqlCommand cmdmobile = new SqlCommand("update tbl_Mobile set status = 'Sold' where IMEINO = @IMEINO",conn))
                        {
                            cmdmobile.Parameters.AddWithValue("@IMEINO", IMEI);
                            cmdmobile.ExecuteNonQuery();
                            using (SqlCommand cmdGetModId = new SqlCommand("SELECT ModID FROM tbl_Model WHERE ModelNum = @modNum", conn))
                            {
                                cmdGetModId.Parameters.AddWithValue("@modNum", lblmodnum.Text);
                                int modID = Convert.ToInt32(cmdGetModId.ExecuteScalar());

                                using (SqlCommand cmdGetQty = new SqlCommand("SELECT AvailableQty FROM tbl_Model WHERE ModelNum = @modNum", conn))
                                {
                                    cmdGetQty.Parameters.AddWithValue("@modNum", lblmodnum.Text);
                                    int Aquantity = Convert.ToInt32(cmdGetQty.ExecuteScalar());
                                    using (SqlCommand cmdmodel = new SqlCommand("update tbl_Model set AvailableQty =  @Aquantity -1 where ModID = @modID ", conn))
                                    {
                                        cmdmodel.Parameters.AddWithValue("@Aquantity", Aquantity);
                                        cmdmodel.Parameters.AddWithValue("@modID", modID);
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
