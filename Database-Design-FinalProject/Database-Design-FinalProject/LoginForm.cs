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
using Npgsql;

namespace Database_Design_FinalProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            userBox.Text = "";
            passBox.Text = "";

        }

        public static string profileName = "";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            bool currentUser = false;


            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=ProjectDB;User Id=jong;Password=GJon;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();

            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            String sql, Output = "", tempName, tempPass;
            comm.CommandText = "Select Username, Password from Account";
            NpgsqlDataReader dr = comm.ExecuteReader();


            while (dr.Read())
            {
                Output = Output + dr.GetValue(0) + " - " + dr.GetValue(1) + "\n";
                tempName = "" + dr.GetValue(0);
                tempPass = "" + dr.GetValue(1);

                if (("" + userBox.Text == tempName) && ("" + passBox.Text == tempPass)) //Checks if there is a matching pair of username and password in the database.
                {
                    currentUser = true;
                    profileName = tempName;
                }
            }

            
            if (currentUser == true) //If there is a match, login is successful. Continue to profile.
            {
                MessageBox.Show("Login Successful.");

                this.Hide(); // Hides the current form.
                ProfileForm profForm = new ProfileForm();
                profForm.Closed += (s, args) => this.Close(); //Will close the first form if profile is closed
                profForm.Show(); //Show Prof form
            }
            else    //No match, Invalid Login.
            {
                MessageBox.Show("Invalid Login.");
            }
            

            /*if (currentUser == true)
            {
                MessageBox.Show("Valid Login.");
            }
            else
                MessageBox.Show("Invalid Login.");
            */

            //Close connections to database.
            dr.Close();
            comm.Dispose();
            conn.Close();

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            RegisterForm regForm = new RegisterForm();
            regForm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}