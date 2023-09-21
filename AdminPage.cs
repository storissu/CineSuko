using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;

namespace CineSuko
{
    public partial class AdminPage : Form
    {
        CinemaHall cinemahall;
        public AdminPage()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        #region Properties

        #endregion

        #region Event
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < LoginPage.company.movies.Count(); i++)
            {

                Label lbl2 = new Label()
                {

                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(14, 62 + 40 * i),
                    Name = "lbl2",
                    Text = LoginPage.company.movies[i].getMovieName(),
                    Font = new Font("MS Reference Sans Serif", 10, FontStyle.Regular),


                };

                panel1.Controls.Add(lbl2);

                Button movie = new Button
                {

                    Width = 120,
                    Height = 25,
                    Left = 200,
                    Top = 60 + 40 * i,
                    BackColor = Color.Silver,
                    Tag = null,
                    Text = "- DELETE MOVIE",
                    ForeColor = Color.AliceBlue,
                    Font = new Font("MS Reference Sans Serif", 7, FontStyle.Regular),

                };
                movie.Name = Convert.ToString(LoginPage.company.movies[i].getMovieId());
                movie.Click += movie_click;
                panel1.Controls.Add(movie);

                void movie_click(object sender1, EventArgs e1)
                {

                    Button btn = (Button)sender1;
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    for (int x = 0; x < LoginPage.company.getMovies().Count(); x++)
                    {

                        Movie tempmovie = LoginPage.company.movies[x];
                      
                        if (movie.Name == Convert.ToString(tempmovie.getMovieId()))
                        {

                            if (tempmovie.getMovieName().Equals("NO MOVIE            "))
                            {
                                string message = "Please Choose a Valid Movie to Delete";
                                MessageBox.Show(message);
                                break;
                            }
                             int hal = Convert.ToInt32(LoginPage.company.movies[x].getWhichHall()) - 1;
                            LoginPage.company.cinemahalls[hal].setAvailable(true);

                            LoginPage.company.movies[x].setNull();
                           
                            movie.Text = "DELETED";
                            movie.BackColor = Color.DarkRed;

                            lbl2.Text = LoginPage.company.movies[x].getMovieName();  

                         
                            string query2 = "update movies set name = '" + LoginPage.company.movies[x].getMovieName() + "', description = '" + LoginPage.company.movies[x].getDescription() + "', release_date = '" +
                                LoginPage.company.movies[x].getReleaseDate() + "', director = '" + LoginPage.company.movies[x].getDirector() + "', movie_type = '" + LoginPage.company.movies[x].GetType() + "', rate = '" +
                                Convert.ToDouble(LoginPage.company.movies[x].getImdbRate()) + "' , poster = 'db.jpg', hal_no =  '" + LoginPage.company.movies[x].getWhichHall() + "' where movie_id = '" + tempmovie.getMovieId() + "' ";

                           
                            SqlCommand command2 = new SqlCommand(query2, con);

                            command2.ExecuteNonQuery();



                        }


                    }

                }


            }
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string selected = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            DateTime date = dateTimePicker1.Value.Date;


            
            cinemahall = LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1];

            for (int i = 0; i < cinemahall.seats.Count(); i++)
            {
                cinemahall.seats[i].setAvailable("true");
            }

            SqlConnection con = new SqlConnection(conString); 
            con.Open();
            string query = "select * from seatdate";
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();

            using (SqlDataReader temp = command.ExecuteReader())
            {
                while (temp.Read())
                {
                    string gender = temp[0].ToString();
                    string seat_no = temp[1].ToString();
                    string date_current = temp[2].ToString();
                    string hall_no = temp[3].ToString();


                    if (Convert.ToString(date).Equals(date_current) && hall_no.Equals(Convert.ToString(cinemahall.getHallId())))
                    {
                        if (temp[0].Equals("f"))
                        {
                            cinemahall.seats[Convert.ToInt32(seat_no)].setAvailable("false_f");
                        }
                        else
                        {
                            cinemahall.seats[Convert.ToInt32(seat_no)].setAvailable("false_m");
                        }

                    }

                }
            } //admin sayfasında dolu olan koltukları tablodan çekip dolduruyor
         
            //SEATS
            string red = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\redseat.png";
            string blue = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blueseat.png";
            string black = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blackseat.png";

            //SEATS
            Image takenseat_female = Image.FromFile(red);
            Image takenseat_male = Image.FromFile(blue);
            Image availableseat = Image.FromFile(black);

            int s = 1;
            Image image;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (cinemahall.seats[s - 1].getAvailable().Equals("true"))
                    {
                        image = availableseat;
                    }
                    else
                    {
                        if (cinemahall.seats[s - 1].getAvailable().Equals("false_f"))
                        {
                            image = takenseat_female;
                        }
                        else if (cinemahall.seats[s - 1].getAvailable().Equals("false_m"))
                        {
                            image = takenseat_male;
                        }
                        else
                        {
                            image = availableseat;
                        }

                    }
                    if (j == 6 && i != 0)
                        continue;

                    if (i >= 4 && (j < i - 3 || j > 15 - i))
                        continue;

                    Button seat = new Button
                    {
                        Width = 35,
                        Height = 35,
                        Left = 8 + (j * 35),
                        Top = 15 + (i * 35),
                        BackgroundImage = image,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = null
                    };
                    seat.Name = Convert.ToString(LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[s - 1].getSeatId());

                    
                    groupBox1.Controls.Add(seat);
                    s++;

                    seat.Click += seat_click;

                    void seat_click(object sender1, EventArgs e1)
                    {
                        Button btn = (Button)sender1;

                        for (int x = 0; x < LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats.Count(); x++)
                        {

                            Seat tempseat = LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x];

                            if (Convert.ToString(tempseat.getSeatId()) == seat.Name)
                            {
                                if (tempseat.getAvailable().Equals("true")) //Boşsa doldur 
                                {
                                    if (radioButton1.Checked)//(female)
                                    {
                                        btn.BackgroundImage = takenseat_female;
                                        LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].setAvailable("false_f");

                                        

                                        string query1 = "insert into seatdate values('" +"f" + "', '"+(LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].getSeatId() - 1)+ "','"+date +"', '" +cinemahall.getHallId()+ "')";
                                        SqlCommand command1 = new SqlCommand(query1, con);
                                        command1.ExecuteNonQuery();
                                    }
                                    else if (radioButton3.Checked)//(male)
                                    {
                                        btn.BackgroundImage = takenseat_male;
                                        LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].setAvailable("false_m");

                                        string query1 = "insert into seatdate values('" + "m" + "', '" + (LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].getSeatId() - 1) + "','" + date + "', '" + cinemahall.getHallId() + "')";
                                        SqlCommand command1 = new SqlCommand(query1, con);
                                        command1.ExecuteNonQuery();                                    
                                    }
                                }
                                else 
                                {
                                    btn.BackgroundImage = availableseat;
                                    LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].setAvailable("true");
                                    int se = (Convert.ToInt32(LoginPage.company.cinemahalls[Convert.ToInt32(selected) - 1].seats[x].getSeatId()) - 1);

                                    string query2 = "delete from seatdate where seat = '" + se + "'";

                                    SqlCommand command2 = new SqlCommand(query2, con);
                                    command2.ExecuteNonQuery();
                                }//Koltuk doluysa boşalt

                            }
                        }
                    }

                }
            } //koltukları bastırma
        }

        #endregion

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString); //fiyat güncellemesi
            con.Open();
            string query = "TRUNCATE TABLE price";
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();

            string new_price_r = textBox2.Text;
            string new_price_s = textBox1.Text;
           

            if (new_price_r.Length==0 && new_price_s.Length!=0)
            {
                
                LoginPage.company.setPriceStudent(new_price_s);
                new_price_r = LoginPage.company.getPriceRegular();
                string message = "Operation Succesfully Done";
                MessageBox.Show(message);
            }
            else if (new_price_r.Length != 0 && new_price_s.Length == 0)
            {
                new_price_s= LoginPage.company.getPriceStudent();
                LoginPage.company.setPriceRegular(new_price_r);
                string message = "Operation Succesfully Done";
                MessageBox.Show(message);
            }
            else if (new_price_r.Length != 0 && new_price_s.Length != 0)
            {
                LoginPage.company.setPriceStudent(new_price_s);
                LoginPage.company.setPriceRegular(new_price_r);
                string message = "Operation Succesfully Done";
                MessageBox.Show(message);
            }
            else
            {
                new_price_r = "$" + LoginPage.company.getPriceRegular();
                new_price_s = "$" + LoginPage.company.getPriceStudent();
            }

            string query1 = "insert into price values('" + new_price_s + "', '" + new_price_r + "')";
            SqlCommand command1 = new SqlCommand(query1, con);
            command1.ExecuteNonQuery();

        }

        private void button3_Click(object sender, EventArgs e)
        {
         

            List<String> available_halls = new List<string>();
            for (int i = 0; i < LoginPage.company.cinemahalls.Count(); i++)
            {
                if (LoginPage.company.cinemahalls[i].getAvailable())
                {
                    available_halls.Add(Convert.ToString(i + 1));
                }
                
            }
            

            if (available_halls.Count()==0)
            {
                string message = "This Operation Can't Be Done. There Are Not Enough Cinema Halls";
                MessageBox.Show(message);
            }
            else
            {
                AddMovie selectionpage = new AddMovie(available_halls);
                //this.Visible = false;
                selectionpage.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            Purchases  purchases = new Purchases();
            //this.Visible = false;
            purchases.ShowDialog();
           // this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoginPage page = new LoginPage();
            this.Visible = false;
            page.ShowDialog();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminPage adminPage = new AdminPage(); 
        ;
            this.Visible = false;
            this.Close();
            adminPage.ShowDialog();
        }
    }
}
