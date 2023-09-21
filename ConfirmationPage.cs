using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using Dnp.Net;

namespace CineSuko
{
    public partial class ConfirmationPage : Form
    {
        Movie movie;
        Seat seat;
        Button clicked;
        string chall_id;
        DateTime date;

        //MAİL İCİN
        string SenderMail;
        string SenderPassword;
        string Subject;
        string Content;
        string ReceiversFile;
        int AllLines;
        string ReceiverName;
        Boolean isSuccess;

        public ConfirmationPage()
        {
            InitializeComponent();
        }

        public ConfirmationPage(Movie m, Seat s, String challid, Button clicked1, DateTime d)
        {
            InitializeComponent();
            movie = m;
            seat = s;
            chall_id = challid;
            clicked = clicked1;
            date = d;

        }
        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void ConfirmationPage_Load(object sender, EventArgs e)
        {
          

            Label lbl1 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 24),
                Name = "lbl1",
                Text = LoginPage.customer.getFirstname() + " " + LoginPage.customer.getLastname(),
                Font = new Font("Microsoft Sans Serif",8 , FontStyle.Bold),


            };
            this.Controls.Add(lbl1);
            String gender;
            if (LoginPage.customer.getGender().Equals("f"))
            {
                gender = "Female";
            }
            else
            {
                gender = "Male";
            }

            Label lbl2 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 41),
                Name = "lbl2",
                Text = gender,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl2);

            Label lbl3 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 58),
                Name = "lbl3",
                Text = movie.getMovieName(),
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl3);

            Label lbl4 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 77),
                Name = "lbl4",
                Text = clicked.Name,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl4);

            Label lbl5 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 97),
                Name = "lbl5",
                Text = chall_id,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl5);

            string price;
            string status;
            if (LoginPage.customer.getIsStudent().Equals("true"))
            {
                price = LoginPage.company.getPriceStudent() ;
                status = "Student";
            }
            else
            {
                price = LoginPage.company.getPriceRegular();
                status = "Regular";
            }


            Label lbl6 = new Label()
            {

                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 115),
                Name = "lbl6",
                Text = status,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl6);

            Label lbl7 = new Label()
            {

                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 131),
                Name = "lbl7",
                Text = price,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),


            };
            this.Controls.Add(lbl7);


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {          
            string red = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\redseat.png";
            string blue = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blueseat.png";
            string black = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\oykusuko@outlook.com\\CineSuko\\Resources\\blackseat.png";

            //SEATS
            Image takenseat_female = Image.FromFile(red);
            Image takenseat_male = Image.FromFile(blue);
            Image availableseat = Image.FromFile(black);

            //confirm button
            Console.WriteLine(seat.getAvailable());

            if (LoginPage.customer.getGender().Equals("f"))
            {
                clicked.BackgroundImage = takenseat_female;
            }
            else
            {
                clicked.BackgroundImage = takenseat_male;
            }
            ConfirmationPage.ActiveForm.Close();

            Console.WriteLine(seat.getSeatId() + " " + seat.getAvailable());

          
            if (LoginPage.customer.getGender().Equals("f"))
            {
                LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[seat.getSeatId() - 1].setAvailable("false_f");
                Console.WriteLine("c1 = " + LoginPage.company.cinemahalls[0].seats[2].getAvailable());
            }
            else
            {
                LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].seats[seat.getSeatId() - 1].setAvailable("false_m");
                Console.WriteLine("c1 = " + LoginPage.company.cinemahalls[0].seats[2].getAvailable());
            }



            SqlConnection con = new SqlConnection(conString);
            con.Open();

            string query = "insert into seatdate values('" + LoginPage.customer.getGender() + "', '" + (seat.getSeatId() - 1) + "','" + Convert.ToString(date) + "', '" + chall_id + "')";
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();


            string query2 = "insert into purchases values('" + LoginPage.customer.getId() + "', '" + LoginPage.customer.getFirstname() + "','" + LoginPage.customer.getLastname() + "', '" + movie.getMovieName() + "','"+ seat.getSeatId() + "')";
            SqlCommand command2 = new SqlCommand(query2, con);
            command2.ExecuteNonQuery();


            //mail

            const int MaxAttemps = 3;
            int Attemps;
            int progress = 0;
            SenderMail = "test.innosa@gmail.com";
            SenderPassword = "ribhcnambgfripzb";
            Subject = "CINEMA SUKO TICKET";
            Content = " Hello, " + LoginPage.customer.getFirstname() + " Here is your ticket informations : \n Movie Name : " + movie.getMovieName() + "\n Seat Number : " +
                clicked.Name + "\n Cinema Hall : " + LoginPage.company.cinemahalls[Convert.ToInt32(chall_id) - 1].getHallId() + "\n Date : " + date;

            try
            {
                
                Attemps = 0;
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(SenderMail),
                    Subject = Subject
                };

                mail.To.Add(LoginPage.customer.getEmail());
                ReceiverName = "su";

                mail.Body = Content;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential(SenderMail, SenderPassword),
                    EnableSsl = true
                };
            Retry:
                try
                {
                    Attemps++;
                    SmtpServer.Send(mail);
                    progress++;
                    isSuccess = true; 
                }
                catch (Exception ep)
                {
                    if (Attemps <= MaxAttemps)
                    {
                        System.Threading.Thread.Sleep(Attemps * 500);
                        goto Retry;
                    }
                    MessageBox.Show("Posta gönderilemiyor: " + ReceiverName + "\n" + ep.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isSuccess = false;
                   
                }
               
            }
            catch (Exception ex)
            {
                isSuccess = false;
                string message = ex.Message;
                string caption = "Hata";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
            }
            try
            {
                SenderMail = "test.innosa@gmail.com";
                SenderPassword = "ribhcnambgfripzb";
                Subject = "slm";
                Content = "slm";
              
                if (string.IsNullOrEmpty(SenderMail) || string.IsNullOrEmpty(SenderPassword))
                {
                    string message = "Önce gönderenin kimlik bilgilerini (posta ve şifre) ayarlamanız gerekir!";
                    string caption = "Hata";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(Content))
                {
                    string message = "İçeriği boş olan bir e-posta gönderemezsiniz!";
                    string caption = "Hata";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }
                else
                {
                    string message = "Operation succesful! Your ticket information has sent yo your email address.";
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string caption = "Hata";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
            }






        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("ddd");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeatSelection page = new SeatSelection();
            this.Visible = false;
            page.ShowDialog();
            this.Close();
        }
    }
}
