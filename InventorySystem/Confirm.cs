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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InventorySystemCsharp
{  
    public partial class Confirm : Form
    {
        public Home MyParent { get; set; }
        public Confirm()
        {
            InitializeComponent();
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = MyParent.ItemList;
        }

        private void Confirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyParent.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyParent.ItemList = "";
            MyParent.TotalPrice = 0;
            MyParent.UpdateQuery = "";
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                userdetail user = new userdetail();

                string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;SqlConnection myConn = new SqlConnection(connectionString);
                //string query = "INSERT INTO `orders`(`user`,`details`,`price`) VALUES('"+user.getUname()+"','"+MyParent.ItemList+"',"+MyParent.TotalPrice+");"+MyParent.UpdateQuery+"";
                string query = "INSERT INTO orders ([user], details, price, isDeleted, isPaid) VALUES ('" + user.getUname() +"', '"+ Convert.ToString(MyParent.ItemList) + "', " + Convert.ToInt32(MyParent.TotalPrice) + ", 0, 0);" + MyParent.UpdateQuery+"";
                SqlCommand cmd = new SqlCommand(query, myConn);
                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();
                MessageBox.Show("Success");
                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
