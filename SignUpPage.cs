using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CineSuko
{
    public partial class SignUpPage : Form
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        public static bool isUsernameTaken(string username)
        {
            bool taken = false;
            for (int i = 0; i < LoginPage.company.customers.Count(); i++)
            {
                if (LoginPage.company.customers[i].getUsername().Equals(username))
                {
                    taken = true;
                    break;
                }             
            }

            return taken;

        }
        private void SignUpPage_Load(object sender, EventArgs e)
        {
            

        }

        private void label2_Click(object sender, EventArgs e)
        {
          
        }

        private void notstudentcheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        
            if (checkBox1.Checked)
            {
                textBox5.UseSystemPasswordChar = false; 
            }
            else
            {
                textBox5.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string lastname = textBox2.Text;
            string email = textBox3.Text;
            string username = textBox4.Text;
            string password = textBox5.Text;
            string gender; 
            string phone = maskedTextBox1.Text;
            string status;
            DateTime birthdate = dateTimePicker1.Value.Date;
            if (studentcheck.Checked)
            {
                status = "true";
            }
            else
            {
                status = "false";
            }

            if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("Female"))
            {
                gender = "f";
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("Male"))
            {
                gender = "m";
            }
            else
            {
                gender = "m";
            }

            int customer_id = LoginPage.company.customers[LoginPage.company.customers.Count() - 1].getId() + 1;
            bool signup = false;
            if (isUsernameTaken(username)== false)
            {
                signup = true;

                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = "insert into customers values('" + name + "','" + lastname + "', '" + birthdate + "' , '"+phone+ "' , '"+email+"' , '"+username+"', '"+password+"', '"+gender+"', '"+status+"' )";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();

            
            }
            else if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email)
                || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(status))
            {
                signup = false;

                string message = "Please fill the blank spaces!";
                MessageBox.Show(message);
            }
            else
            {
                signup = false;
                string message = "This username is already taken!";
                MessageBox.Show(message);
            }

            

            if (signup)
            {
                LoginPage page = new LoginPage();
                this.Visible = false;
                page.ShowDialog();
                this.Close();
            }
            

        }
    }
}
