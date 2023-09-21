using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineSuko
{
    public class Seat
    {
        int seat_no;
        string isAvailable;
        string price_student;
        string price_regular;
        string available_in;
        public Seat(int seat_no)
        {
            this.seat_no = seat_no;
            
        }

        public void setAvailable(string m)
        {
             isAvailable = m;
        }

        //public void setAvailableIn( string d, string m)
        //{
        //    available_in = m;
        //}

        //public String getAvailableIn(string d)
        //{
        //    return available_in;
        //}

        public void setPriceStudent(string p)
        {
            price_student = p;
        }
        public String getPriceStudent()
        {
            return price_student;
        }

        public void setPriceRegular(string p)
        {
            price_regular = p;
        }
        public String getPriceRegular()
        {
            return price_regular;
        }
        public String getAvailable()
        {
            return isAvailable;
        }

        

        public int getSeatId()
        {
            return seat_no;
        }



    }
}
