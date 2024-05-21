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
    public partial class Home : Form
    {
        public string ItemList="";
        public float TotalPrice=0;
        public string UpdateQuery="";


        public Home()
        {
            InitializeComponent();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            FillComboBox();
            ItemList = "";
            TotalPrice = 0;
            UpdateQuery = "";

            userdetail user = new userdetail();
            label9.Text = user.getUname();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;SqlConnection myConn = new SqlConnection(connectionString);
                String query = "select * from Products where model='" + comboBox1.Text + "' AND part='" + comboBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, myConn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        

        void FillComboBox()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;SqlConnection myConn = new SqlConnection(connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT model from Products where isDeleted = 0", myConn);
            DataSet dt = new DataSet();
            sda.Fill(dt);

            comboBox1.DataSource = dt.Tables[0];
            comboBox1.DisplayMember = "model";
            comboBox1.ValueMember = "model";

            SqlDataAdapter sda1 = new SqlDataAdapter("select DISTINCT part from Products where isDeleted = 0", myConn);
            DataSet dt1 = new DataSet();
            sda1.Fill(dt1);

            comboBox2.DataSource = dt1.Tables[0];
            comboBox2.DisplayMember = "part";
            comboBox2.ValueMember = "part";

            myConn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox6.Text) >= int.Parse(textBox7.Text))
                {
                    ItemList += textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + " " + textBox5.Text + "*" + textBox7.Text+Environment.NewLine;
                    TotalPrice += float.Parse(textBox5.Text) * float.Parse(textBox7.Text);
                    UpdateQuery += "update Products set instock='" + (int.Parse(textBox6.Text) - int.Parse(textBox7.Text)) + "' where id='" + textBox1.Text + "';";
                    String msg = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + "*" + textBox7.Text;
                    MessageBox.Show(msg+Environment.NewLine+"Added to Cart");
                }
                else
                {
                    MessageBox.Show("Not Enough Items in Stock");
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Enter in Correct Format");
            }
            catch(Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ItemList == "")
            {
                MessageBox.Show("No Items Selected");
            }
            else
            {
                var childform = new Confirm();
                childform.MyParent = this;
                childform.Show();
                this.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyOrders orders = new MyOrders();
            this.Hide();
            orders.Show();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
