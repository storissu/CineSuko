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
    public partial class SeatSelection : Form
    {
        Movie movie;
        String chall_id;
        CinemaHall cinemahall;
        DateTime date;
        public SeatSelection()
        {
            InitializeComponent();
        }
        public SeatSelection(Movie m, String hall, DateTime d)
        {
            InitializeComponent();
            movie = m;
            chall_id = hall;
            date = d;

        }

        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        private void SeatSelection_Load(object sender, EventArgs e)
        {


            for (int i = 0; i < LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats.Count(); i++)
            {
                LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[i].setAvailable("true");
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

                    if (Convert.ToString(date).Equals(date_current) && hall_no.Equals(Convert.ToString(LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].getHallId())))
                    {
                        if (temp[0].Equals("f"))
                        {
                            LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[Convert.ToInt32(seat_no)].setAvailable("false_f");
                        }
                        else
                        {
                            LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[Convert.ToInt32(seat_no)].setAvailable("false_m");
                        }
                    }
                }
            }


            string programfiles1 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\" + movie.getPoster();
            PictureBox pictureBox1 = new PictureBox()
            {

                BackColor = Color.Black,
                ImageLocation = programfiles1,
                Location = new Point(10, 60),
                Name = "pictureBox1",
                Size = new Size(200, 300),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Panel back = new Panel
            {

                Width = 120, //18
                Height = 579, //18
                Left = 51,
                Top = 0,
                BackColor = Color.Chocolate,
                Tag = null,
                // BackgroundImage = Image.FromFile("..\\..\\resources\\darkback.jpg"),
                //BackgroundImageLayout = ImageLayout.Stretch,
            };

            this.Controls.Add(back);


            string price;
            string status;
            this.Controls.Add(pictureBox1);
            pictureBox1.BringToFront();
            if (LoginPage.customer.getIsStudent().Equals("true"))
            {
                price = LoginPage.company.getPriceStudent();
                status = "STUDENT";
            }
            else
            {
                price = LoginPage.company.getPriceRegular();
                status = "REGULAR";
            }

            Label lbl = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 15),
                Name = "lbl",
                Text = "Movie : " + movie.getMovieName(),
                Font = new Font("Serif", 9, FontStyle.Bold),

            };
            this.Controls.Add(lbl);

            Label lbl2 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 45),
                Name = "lbl2",
                Text = "Director : " + movie.getDirector(),
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbl2);

            Label lbl3 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 75),
                Name = "lbl3",
                Text = "IMDB Rate : " + movie.getImdbRate() + "/10",
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbl3);


            Label lbl4 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 105),
                Name = "lbl4",
                Text = "Genre : " + movie.getMovieType(),
                Font = new Font("Serif", 9, FontStyle.Bold),

            };
            this.Controls.Add(lbl4);


            Label lbl5 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 135),
                Name = "lbl4",
                Text = "Release in : " + movie.getReleaseDate(),
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbl5);


            Label lbl6 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 165),
                Name = "lbl6",
                Text = "Cinama Hall No : " + movie.getWhichHall(),
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbl6);

            Label lbl7 = new Label()
            {

                BackColor = Color.Orange,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 195),
                Name = "lbl7",
                Text = "Description : " + movie.getDescription(),
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbl7);

            Label lbls = new Label()
            {

                BackColor = Color.Coral,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 225),
                Name = "lbls",
                Text = "Ticket (" + status + ") : " + price,
                Font = new Font("Serif", 9, FontStyle.Bold),


            };
            this.Controls.Add(lbls);

            cinemahall = LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1];

            string red = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\redseat.png";
            string blue = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blueseat.png";
            string black = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blackseat.png";

            //SEATS
            Image takenseat_female = Image.FromFile(red);
            Image takenseat_male = Image.FromFile(blue);
            Image availableseat = Image.FromFile(black);

            PictureBox blackseat = new PictureBox()
            {

                BackColor = Color.Transparent,
                ImageLocation = black,
                Location = new Point(220, 260),
                Name = "blackseat",
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(blackseat);
            blackseat.BringToFront();

            PictureBox redseat = new PictureBox()
            {

                BackColor = Color.Transparent,
                ImageLocation = red,
                Location = new Point(220, 305),
                Name = "redseat",
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(redseat);
            blackseat.BringToFront();

            PictureBox blueseat = new PictureBox()
            {

                BackColor = Color.Transparent,
                ImageLocation = blue,
                Location = new Point(220, 350),
                Name = "blueseat",
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(blueseat);
            blackseat.BringToFront();

            Label lbl8 = new Label()
            {

                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(265, 360),
                Name = "lbl8",
                Text = ": Taken Seat by Male",
                Font = new Font("Serif", 7, FontStyle.Bold),


            };
            this.Controls.Add(lbl8);

            Label lbl9 = new Label()
            {

                BackColor = Color.Red,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(265, 315),
                Name = "lbl8",
                Text = ": Taken Seat by Female",
                Font = new Font("Serif", 7, FontStyle.Bold),


            };
            this.Controls.Add(lbl9);

            Label lbl10 = new Label()
            {

                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(265, 275),
                Name = "lbl10",
                Text = ": Available Seat",
                Font = new Font("Serif", 7, FontStyle.Bold),


            };
            this.Controls.Add(lbl10);

            Console.WriteLine("cinemahal is    =     " + LoginPage.company.cinemahalls[0].seats[8].getAvailable());

            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G" };
            bool isTaken;
            int s = 1;
            Image image;
            for (int i = 0; i < 7; i++)
            {
                Label lbl11 = new Label()
                {

                    BackColor = Color.DarkSlateGray,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(5, 41 + 45 * i),
                    Name = "lbl11",
                    Text = alphabet[i],
                    Font = new Font("Serif", 7, FontStyle.Bold),


                };
                groupBox1.Controls.Add(lbl11);

                for (int j = 0; j < 13; j++)
                {
                    if (i == 0)
                    {
                        Label lbl12 = new Label()
                        {

                            BackColor = Color.DarkSlateGray,
                            ForeColor = Color.White,
                            AutoSize = true,
                            Location = new Point(40 + 45 * j, 10),
                            Name = "lbl12",
                            Text = Convert.ToString(j + 1),
                            Font = new Font("Serif", 7, FontStyle.Bold),


                        };
                        groupBox1.Controls.Add(lbl12);
                    }
                    if (cinemahall.seats[s - 1].getAvailable().Equals("true"))
                    {
                        isTaken = false;
                        image = availableseat;

                    }
                    else
                    {

                        if (cinemahall.seats[s - 1].getAvailable().Equals("false_f"))
                        {

                            image = takenseat_female;
                            Console.WriteLine(cinemahall.seats[s - 1].getAvailable());
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
                        Width = 45,
                        Height = 45,
                        Left = 25 + (j * 45),
                        Top = 25 + (i * 45),
                        BackgroundImage = image,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = null
                    };
                    seat.Name = Convert.ToString(LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[s - 1].getSeatId());
                    //seat.Click += seat_Click;


                    groupBox1.Controls.Add(seat);
                    s++;

                    seat.Click += seat_click;

                    void seat_click(object sender1, EventArgs e1)
                    {
                        Button btn = (Button)sender1;




                        int h = 0;
                        for (int k = 0; k < 7; k++)
                        {
                            for (int l = 0; l < 13; l++)
                            {
                                if (l == 6 && k != 0)
                                    continue;

                                if (k >= 4 && (l < k - 3 || l > 15 - k))
                                    continue;

                                Seat tempseat = LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[h];

                                if (Convert.ToString(tempseat.getSeatId()) == seat.Name)
                                {
                                    if (tempseat.getAvailable().Equals("true"))
                                    {
                                        btn.Name = alphabet[k] + Convert.ToString(l + 1);
                                        Console.WriteLine("seat  is = " + tempseat.getSeatId() + " " + chall_id);
                                        //seatno , moviename göndercem buraya confirmation pageye
                                        ConfirmationPage conf = new ConfirmationPage(movie, tempseat, chall_id, btn, date);
                                        //this.Visible = false;
                                        conf.ShowDialog();
                                        //this.Close();
                                    }
                                    else
                                    {
                                        string message = "This Seat Has Already Taken Please Choose Another One";
                                        MessageBox.Show(message);
                                    }


                                }
                                h++;
                            }


                        }

                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectionPage page1 = new SelectionPage();
            this.Visible = false;
            this.Close();
            page1.ShowDialog();

        }
    }
}
