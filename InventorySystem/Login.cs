using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.Compression;
using System.IO;
 

namespace InventorySystemCsharp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
            {
            try
            {


                string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;SqlConnection myConn = new SqlConnection(connectionString);

                if(bunifuMetroTextbox1.Text != "" && bunifuMetroTextbox2.Text != "")
                {
                    string queryFilter = "Where username = '" + Convert.ToString(bunifuMetroTextbox1.Text.Trim()) + "'and password = '" + Convert.ToString(bunifuMetroTextbox2.Text.Trim()) + "'";
                    string query = @"select * from users ";

                    string CMD = query + queryFilter;
                    DataTable dt = GetTableValue(CMD);
              
                    if (dt != null)
                    {
                    
                        if (dt.Rows.Count == 1)
                        {
                            userdetail user = new userdetail();
                   
                            user.setUname(dt.Rows[0]["username"].ToString());




                            if ((string)dt.Rows[0]["usertype"].ToString() == "member")
                            {
                                Home home = new Home();
                                this.Hide();
                                home.Show();
                            }
                            if ((string)dt.Rows[0]["usertype"].ToString() == "manager")
                            {
                                Manager_home manager = new Manager_home();
                                this.Hide();
                                manager.Show();
                            }
                            if ((string)dt.Rows[0]["usertype"].ToString() == "admin")
                            {
                                Admin_home admin = new Admin_home();
                                this.Hide();
                                admin.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Check Again");
                    }

                }



            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public static DataTable GetTableValue(string Cmd)
        {
            SqlConnection myConn = new SqlConnection("data source=(local);initial catalog=IMS;user id=sa;password=Indigo@123;");
            DataTable t1 = new DataTable();

            SqlCommand myCommand = new SqlCommand(Cmd, myConn);

            try
            {
                myCommand.Connection.Open();
                //myCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SqlCommandTimeOut"]);
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    t1.Load(dr);
                }
                dr.Dispose();
                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                myCommand.Connection.Dispose();
                myCommand.Connection.Close();
            }
            return t1;
        }
    }
}
