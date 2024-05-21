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
using MySql.Data.MySqlClient;

namespace InventorySystemCsharp
{
    public partial class User_list : Form
    {
        public User_list()
        {
            InitializeComponent();
        }

        //fill grid view method
        void Filluserlist()
        {
            //MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;SqlConnection myConn = new SqlConnection(connectionString);
            SqlDataAdapter list = new SqlDataAdapter("select first,last,username,phone,usertype from users ", myConn);
            DataTable dtlist = new DataTable();
            list.Fill(dtlist);
            users_list.DataSource = dtlist;
        }

        private void User_list_Load(object sender, EventArgs e)
        {
            Filluserlist();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Admin_home admin = new Admin_home();
            admin.Show();
            this.Close();
        }
    }
}
