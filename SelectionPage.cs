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

namespace CineSuko
{
    public partial class SelectionPage : Form
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SelectionPage_Load(object sender, EventArgs e)
        {

            int a = 1;
            //filmleri bastırıyo
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string imageposter = LoginPage.company.movies[a - 1].getPoster();

                    string programfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\" + imageposter;
                    Console.WriteLine(programfiles);
                    Button movie = new Button
                    {


                        Width = 183,
                        Height = 252,
                        Left = 60 + (j * 240),
                        Top = 23 + (i * 290),
                        BackColor = Color.Black,
                        Tag = null,
                        Text = LoginPage.company.movies[a - 1].getMovieName(),
                        ForeColor = Color.Brown,
                        BackgroundImage = Image.FromFile(programfiles),
                        BackgroundImageLayout = ImageLayout.Stretch,


                    };

                    movie.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    movie.FlatAppearance.BorderColor = System.Drawing.Color.AntiqueWhite;
                    movie.FlatAppearance.BorderSize = 200;

                    movie.Name = Convert.ToString(LoginPage.company.movies[a - 1].getMovieId());

                    movie.Click += movie_click;
                    Movies.Controls.Add(movie);


                    void movie_click(object sender1, EventArgs e1)
                    {
                        Button btn = (Button)sender1;

                        for (int x = 0; x < LoginPage.company.getMovies().Count(); x++)
                        {

                            Movie tempmovie = LoginPage.company.movies[x];




                            if (movie.Name == Convert.ToString(tempmovie.getMovieId()))
                            {

                                if (tempmovie.getMovieName().Equals("NO MOVIE            "))
                                {
                                    string message = "Please Choose a Valid Movie";
                                    MessageBox.Show(message);
                                    break;
                                }
                                Console.WriteLine("movie name is = " + LoginPage.company.movies[x].getMovieName());

                                SeatSelection seatselectionpage = new SeatSelection(LoginPage.company.movies[x], tempmovie.getWhichHall(), dateTimePicker1.Value.Date);
                                this.Visible = false;
                                seatselectionpage.ShowDialog();
                                this.Close();
                            }

                        }

                    }
                    a++;

                }
            }


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {


                    Panel back = new Panel
                    {

                        Width = 201, //18
                        Height = 270, //18
                        Left = 51 + (j * 240),
                        Top = 12 + (i * 290),
                        BackColor = Color.PowderBlue,
                        Tag = null,
                        // BackgroundImage = Image.FromFile("..\\..\\resources\\back.jpg"),
                        //BackgroundImageLayout = ImageLayout.Stretch,





                    };

                    Movies.Controls.Add(back);

                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginPage loginpage = new LoginPage();
            this.Visible = false;
            this.Close();
            loginpage.ShowDialog();
        }
    }
}
