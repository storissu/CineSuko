using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace CineSuko
{
    public class Company

    {
      //  ArrayList movies = new ArrayList();
        //ArrayList customers = new ArrayList();
        //ArrayList cinemahalls = new ArrayList();
       
        int hall_amount = 15;
        Admin admin;
        public List<Movie> movies = new List<Movie>();
        public List<Customer> customers = new List<Customer>();
        public List<CinemaHall> cinemahalls = new List<CinemaHall>();
        public static int customerCount = 0;
        string price_student;
        string price_regular;

        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
            customerCount++;
        }
       

        public void addCinemaHall(CinemaHall ch)
        {
            cinemahalls.Add(ch);
        }

        public Customer getCustomer(int i)
        {
            return (Customer)customers[i];
        }
        public List<Movie> getMovies()
        {
            return movies;
        }
        public Movie getMovie(int i)
        {
            return movies[i];
        }

        public int getCustomerCount()
        {
            return customerCount;
        }
        //ADMIN OPERATIONS: addMovie, deleteMovie, changePrice, changeSeatStatus
        public void addMovie(Movie movie)
        {
            movies.Add(movie);
        }
        public void deleteMovieBy_Id(int id)
        {
            for (int i = 0; i < movies.Count(); i++)
            {
                if (id == movies[i].getMovieId())
                {
                    movies.RemoveAt(i);
                }
            }           
        }

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
    }
}
