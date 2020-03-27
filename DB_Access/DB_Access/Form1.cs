using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//---------- using
using System.Data;
using System.Data.OleDb;

namespace DB_Access
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string StrConnection;
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string StrPath = Application.StartupPath;
            StrConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + StrPath + @"\DB\DB_Access.accdb;Persist Security Info=False;";
            GetList();
        }
        public void GetList()
        {
            OleDbConnection condb = new OleDbConnection(StrConnection);
            OleDbDataAdapter DataA = new OleDbDataAdapter("select * from Tbl_list",condb);
            DataTable DTabel = new DataTable();
            DataA.Fill(DTabel);
            dataGridView1.DataSource = DTabel;
            ClearText();
        }
        public void ClearText()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        public Boolean Searching(string strID)
        {
            OleDbConnection condb = new OleDbConnection(StrConnection);
            OleDbDataAdapter DataA = new OleDbDataAdapter("select * from Tbl_list where ID=" + strID, condb);
            DataTable DTabel = new DataTable();
            DataA.Fill(DTabel);
            if (DTabel.Rows.Count > 0)
                return true;
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("لطفا داده ها را وارد کنید.");
                return;
            }
            if (!Searching(textBox1.Text))
            {
                OleDbConnection condb = new OleDbConnection(StrConnection);
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = condb;
                // ------ Insert into Tbl_List values(1,'omid','sotooni')
                cmd.CommandText = "Insert into Tbl_List Values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "')";
                condb.Open();
                cmd.ExecuteNonQuery();
                condb.Close();
                GetList();
            }
            else
            {
                MessageBox.Show("شماره کاربری وجود دارد.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Searching(textBox1.Text))
            {
                OleDbConnection condb = new OleDbConnection(StrConnection);
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = condb;
                // ------ Update Tbl_List set FName='Hasan',LName='kami' where ID=1
                cmd.CommandText = "update Tbl_List set FName='" + textBox2.Text + "', LName='" + textBox3.Text + "' where ID=" + textBox1.Text; 
                condb.Open();
                cmd.ExecuteNonQuery();
                condb.Close();
                GetList();
            }
            else
            {
                MessageBox.Show("شماره کاربری وجود ندارد.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.Text.Trim() != "";
            button3.Enabled = textBox1.Text.Trim() != "";
            button4.Enabled = textBox1.Text.Trim() != "";
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (Searching(textBox1.Text))
            {
                OleDbConnection condb = new OleDbConnection(StrConnection);
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = condb;
                // ------ Delete from Tbl_List where ID=1
                cmd.CommandText = "delete from Tbl_List where ID=" + textBox1.Text;
                condb.Open();
                cmd.ExecuteNonQuery();
                condb.Close();
                GetList();
            }
            else
            {
                MessageBox.Show("شماره کاربری وجود ندارد.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection condb = new OleDbConnection(StrConnection);
            OleDbDataAdapter DataA = new OleDbDataAdapter("select * from Tbl_list where ID=" + textBox1.Text, condb);
            DataTable DTabel = new DataTable();
            DataA.Fill(DTabel);
            if(DTabel.Rows.Count>0)
            {
                textBox2.Text = DTabel.Rows[0].Field<string>("FName");
                textBox3.Text = DTabel.Rows[0].Field<string>("LName");
            }
            else
            {
                MessageBox.Show("شماره کاربری وجود ندارد.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection condb = new OleDbConnection(StrConnection);
            OleDbDataAdapter DataA = new OleDbDataAdapter("select * from Tbl_list where FName like '" + textBox2.Text + "%'", condb);
            DataTable DTabel = new DataTable();
            DataA.Fill(DTabel);
            dataGridView1.DataSource = DTabel;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetList();
        }
    }
}
