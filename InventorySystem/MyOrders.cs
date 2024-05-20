using MySql.Data.MySqlClient;
using System;using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class MyOrders : Form
    {
        public MyOrders()
        {
            InitializeComponent();
        }

        private void MyOrders_Load(object sender, EventArgs e)
        {
            userdetail user = new userdetail();
            label9.Text = user.getUname();
            FillMyOrdeers();
        }

        void FillMyOrdeers()
        {
            try
            {
                SqlConnection myConn = new SqlConnection(@"data source=(local);initial catalog=IMS;user id=sa;password=Indigo@123;");
                //string query = "SELECT id,details,price,paid FROM `orders` where user ='"+label9.Text+"'"
                string query = "SELECT id, details, price, isPaid   FROM orders where isDeleted = 0 AND [user] = '" + label9.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query , myConn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                SqlConnection myConn = new SqlConnection(@"data source=(local);initial catalog=IMS;user id=sa;password=Indigo@123;");
                string query = "update orders set isDeleted=1 where id = "+id;
                SqlCommand cmd = new SqlCommand(query, myConn);
                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();
                MessageBox.Show("Order Deleted");

                FillMyOrdeers();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No item Selected");
            }
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Close();
            home.Show();
        }
    }
}
