using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Documents;

namespace CineSuko
{
    public partial class LoginPage : Form
    {
        // public static Company company;
        public static Customer customer;
        public static int id;
        public static Company company = new Company();

        Admin admin = new Admin(1, "admin", "admin", "admin@hotmail.com", "a", "a");




        //matching fonksiyonları
        public static bool isMatching(String username, String password, Company company)
        { //it controls is username and passwords matches for customer
            bool isMatching = false;

            for (int i = 0; i < company.getCustomerCount(); i++)
            {
                if (string.Equals((company.getCustomer(i).getUsername()), username))
                {
                    if (string.Equals((company.getCustomer(i).getPassword()), password))
                    {
                        isMatching = true;
                        customer = company.getCustomer(i);
                    }
                    break;
                }

            }
            return isMatching;

        }


        public LoginPage()
        {
            InitializeComponent();

        }

        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //LOGIN BUTONUNA BASINCA
        private void login_button_Click(object sender, EventArgs e)
        {

            //konsola denemek için yazdırdım önemli değil.
            for (int i = 0; i < company.movies.Count; i++)
            {
                Console.WriteLine(company.movies[i].getMovieName());
            }

            string username = username_box.Text;
            string password = password_box.Text;


            if (username.Equals(admin.getUsername()) && password.Equals(admin.getPassword()))
            {
                AdminPage adminpage = new AdminPage();
                this.Visible = false;
                adminpage.ShowDialog();
                this.Close();
            }
            else if (isMatching(username, password, company))
            {
                SelectionPage selectionpage = new SelectionPage();
                this.Visible = false;
                selectionpage.ShowDialog();
                this.Close();
            }
            else
            {
                string message = "Wrong Username or Password";
                MessageBox.Show(message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_button_Click(object sender, EventArgs e)
        {

            SignUpPage signuppage = new SignUpPage();
            this.Visible = false;
            signuppage.ShowDialog();
            this.Close();

        }


        private void LoginPage_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 6; i++)
            {
                CinemaHall c = new CinemaHall(i);
                company.addCinemaHall(c);
                company.cinemahalls[i - 1].setAvailable(true);


            }

            string price_s = "";
            string price_r = "";

            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                //CUSTOMERS----------------------------
                string query_customers = "select * from customers";
                SqlCommand command_customers = new SqlCommand(query_customers, con);
                command_customers.ExecuteNonQuery();

                using (SqlDataReader temp = command_customers.ExecuteReader())
                {
                    while (temp.Read())
                    {
                        int customer_id = Convert.ToInt32(temp[0]);
                        string name = temp[1].ToString();
                        string lastname = temp[2].ToString();
                        string birthdate = temp[3].ToString();
                        string phone = temp[4].ToString();
                        string email = temp[5].ToString();
                        string username = temp[6].ToString();
                        string password = temp[7].ToString();
                        string gender = temp[8].ToString();
                        string is_student = temp[9].ToString();

                        Customer customer = new Customer(customer_id, name, lastname, birthdate, phone, email, username, password, gender, is_student);

                        company.addCustomer(customer);
                    }


                }//CUSTOMERS DONE------------------------


                //MOVIES------------------------------
                string query_movies = "select * from movies";
                SqlCommand command_movies = new SqlCommand(query_movies, con);
                command_movies.ExecuteNonQuery();

                using (SqlDataReader temp = command_movies.ExecuteReader())
                {
                    while (temp.Read())
                    {
                        int movie_id = Convert.ToInt32(temp[0]);
                        string movie_name = temp[1].ToString();
                        string description = temp[2].ToString();
                        string release_date = temp[3].ToString();
                        string director = temp[4].ToString();
                        string movie_type = temp[5].ToString();
                        double rate = Convert.ToDouble(temp[6].ToString());
                        string poster = temp[7].ToString();
                        string which_hall = temp[8].ToString();


                        Movie movie = new Movie(movie_id, movie_name, description, release_date, director, movie_type, rate, poster, which_hall);
                        if (company.movies.Count() < 6)
                        {
                            company.addMovie(movie);
                        }



                    }
                }//MOVIES DONE------------------------------

                //PRICES------------------------------
                string query_prices = "select * from price";
                SqlCommand command_prices = new SqlCommand(query_prices, con);
                command_prices.ExecuteNonQuery();

                using (SqlDataReader temp = command_prices.ExecuteReader())
                {
                    while (temp.Read())
                    {
                        price_r = temp[1].ToString();
                        price_s = temp[1].ToString();

                    }
                }//PRICES DONE------------------------------
            }
            LoginPage.company.setPriceStudent(price_s);
            LoginPage.company.setPriceRegular(price_r);

            for (int i = 1; i <= 93; i++)
            {
                Seat seat = new Seat(i);
                Seat seat1 = new Seat(i);
                Seat seat2 = new Seat(i);
                Seat seat3 = new Seat(i);
                Seat seat4 = new Seat(i);
                Seat seat5 = new Seat(i);

                LoginPage.company.cinemahalls[0].addSeat(seat);
                LoginPage.company.cinemahalls[1].addSeat(seat1);
                LoginPage.company.cinemahalls[2].addSeat(seat2);
                LoginPage.company.cinemahalls[3].addSeat(seat3);
                LoginPage.company.cinemahalls[4].addSeat(seat4);
                LoginPage.company.cinemahalls[5].addSeat(seat5);

            }

            for (int i = 0; i < company.movies.Count(); i++)
            {
                for (int j = 0; j < company.cinemahalls.Count(); j++)
                { 
                    string hallid = Convert.ToString(company.cinemahalls[j].getHallId());

                    if (company.movies[i].getWhichHall().Equals(hallid))
                    {
                        company.cinemahalls[j].setAvailable(false);
                    }


                }

            }
        }

        private void login_button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }
             
        private void LoginPage_Shown(object sender, EventArgs e)
        {
            username_box.Focus();
        }



        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Application.Exit();
           // Environment.Exit(0);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
             
        }
    }
}
