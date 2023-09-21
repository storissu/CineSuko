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

namespace CineSuko
{
    public partial class Purchases : Form
    {
        public Purchases()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-5N18M5O;Initial Catalog=CINEMA;Integrated Security=True";

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query_customers = "select * from purchases";
            SqlCommand command_customers = new SqlCommand(query_customers, con);
            command_customers.ExecuteNonQuery();

            using (SqlDataReader temp = command_customers.ExecuteReader())
            {
                int a = 0;
                while (temp.Read())
                {
                    int customer_id = Convert.ToInt32(temp[0]);
                    string name = temp[1].ToString();
                    string lastname = temp[2].ToString();
                    string movie = temp[3].ToString();
                    string seat = temp[4].ToString();

                    Label lbl2 = new Label()
                    {

                        BackColor = Color.Transparent,
                        ForeColor = Color.Black,
                        AutoSize = true,
                        Location = new Point(15, 40 + 20 * a),
                        Name = "lbl2",
                        Text = String.Format("{0,-10}  {1,23}  {2,1} {3,32}  {4,40} ", customer_id, name, lastname, movie, seat),
                        Font = new Font("MS Reference Sans Serif", 8, FontStyle.Regular),


                    };
                    panel1.Controls.Add(lbl2);
                    a++;
                }
            }
        }

        private void Purchases_Load(object sender, EventArgs e)
        {
            this.panel1.AutoScroll = true;
        }
    }
}
