using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineSuko
{
    public class CinemaHall //koltuklar array
    {
        int hall_id;
        //string which_movie;
        public List<Seat> seats = new List<Seat>();
        bool isAvailable;


        public CinemaHall(int hall_id)
        {
            this.hall_id = hall_id;
          //  this.seats = seats;
            //this.isAvailable = isAvailable;
        }

        public int getHallId()
        {
            return hall_id;
        }

       // public string getWhichMovie()
       // {
         //   return which_movie;
        //}

        public bool getAvailable()
        {
            return isAvailable;
        }

        public void addSeat(Seat s)
        {
            seats.Add(s);
        }

        public void setAvailable(bool a)
        {
            isAvailable = a;
        }


    }
}
