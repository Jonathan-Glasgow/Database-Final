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

namespace Database_Design_FinalProject
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;

            connetionString = @"Data Source= localhost\SQLEXPRESS; Initial Catalog=ProjectDB; Trusted_Connection=True;";
            cnn = new SqlConnection(connetionString);

            cnn.Open();

            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql;

            //credentials:

            string regUsername = textBox1.Text;
            string regPassword = textBox2.Text;
            string regEmail = textBox3.Text;

            SetMyCustomFormat();
            string regDOB = dateTimePicker1.Text;

            //string regRegion = "Placeholder";
            string regRegion = comboBox1.Text;
    
            sql = $"Insert into Account (Username, Password, Email, DOB, Region) values('{regUsername}', '{regPassword}', '{regEmail}', '{regDOB}', '{regRegion}')";

            command = new SqlCommand(sql, cnn);

            adapter.InsertCommand = new SqlCommand(sql, cnn);
            try
            {
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                this.Hide();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Username is already taken. Try again.");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }
    }
}
