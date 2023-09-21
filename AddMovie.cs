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
using System.Reflection;

namespace CineSuko
{
    public partial class AddMovie : Form
    {
        public static bool isNumeric(string d)
        {
            double result;
            if (double.TryParse(d, out result))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        List<String> available_halls;
        public AddMovie()
        {
            InitializeComponent();
        }

        public AddMovie(List<String>a)
        {
            available_halls = a;
            
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        
        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Click += button1_Click;
            //Button btn = (Button)sender;
            
            string moviename = textBox1.Text;
            string description = textBox2.Text;
            string release = textBox3.Text;
            string director = textBox4.Text;
            string type = textBox5.Text;         
            string hall = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            double rate = 0;
            bool add;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) ||
                string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(this.comboBox2.GetItemText(this.comboBox2.SelectedItem))) 
            {
                add = false;
                string message = "Dont Let Any Blank Spaces Left Please.";
                MessageBox.Show(message);
            }
            else if (isNumeric(Convert.ToString(textBox6.Text)))
            {
                rate = Convert.ToDouble(textBox6.Text);
                add = true;
            }
            
            else
            {
                string message1 = "Invalid Entry, Please Check Again.";
                MessageBox.Show(message1);
                add = false;
            }

           
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (add) //rate kısmı numeric mi dşye bakıyor 
            {
                string query = "select movie_id from movies where hal_no = 'b'";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                int id1 = (int)command.ExecuteScalar();
                LoginPage.company.deleteMovieBy_Id(id1);


                string query2 = "update movies set name = '" + moviename + "' , description ='" + description + "', release_date = '" + release + "', director = '" + director + "', movie_type = '" + type + "' , rate = '" + rate + "', poster = 'db.jpg', hal_no = '" + hall + "' where movie_id = '" + id1 + "' ";
                SqlCommand command2 = new SqlCommand(query2, con);
                command2.ExecuteNonQuery();


                LoginPage.company.cinemahalls[Convert.ToInt32(hall) - 1].setAvailable(false);

                string query_movies = "select movie_id from movies where name = '" + moviename + "'";
                SqlCommand command_movies = new SqlCommand(query_movies, con);
                command_movies.ExecuteNonQuery();

                int id = (int)command_movies.ExecuteScalar();

                Movie movie = new Movie(id, moviename, description, release, director, type, rate, "db.jpg", hall);

                LoginPage.company.addMovie(movie);
                this.Close();
            }
            

            
        


        }

        private void AddMovie_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < available_halls.Count(); i++)
            {
                comboBox2.Items.Add(available_halls[i]);

            }
           
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
