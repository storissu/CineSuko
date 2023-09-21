using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace CineSuko
{
    public class Movie
    {

        int movie_id;
        string movie_name;
        string description;
        string release_date;
        string director;
        string movie_type;
        double imdb_rate;
        string poster;
        string which_hall;
        
        public Movie(int movie_id, string movie_name, string description, string release_date, string director, string movie_type,
           double imdb_rate, string poster, string which_hall)
        {
            this.movie_id = movie_id;
            this.movie_name = movie_name;
            this.description = description;
            this.release_date = release_date;
            this.director = director;
            this.movie_type = movie_type;
            this.imdb_rate = imdb_rate;
            this.poster = poster;
            this.which_hall = which_hall;
        }

        

        public int getMovieId()
        {
            return movie_id;
        }
        public String getMovieName()
        {
            return movie_name;
        }
        public void setMovieName(string m)
        {
            movie_name = m;         
        }

        public String getDescription()
        {
            return description;
        }
        public void setDescription(string m)
        {
            description = m;
        }

        public string getReleaseDate()
        {
            return release_date;
        }
        public void setDate(string m)
        {
            release_date = m;
        }

        public String getDirector()
        {
            return director;
        }
        public void setDirector(string m)
        {
            director = m;
        }
        public String getMovieType()
        {
            return movie_type;
        }
        public void setMovieType(string m)
        {
            movie_type = m;
        }

        public double getImdbRate()
        {
            return imdb_rate;
        }
        public void setImdbRate(double m)
        {
            imdb_rate = m;
        }

        public String getPoster()
        {
            return poster;
        }
        public void setPoster(string m)
        {
            poster = m;
        }
        public void setNull()
        {
            movie_name = "NO MOVIE            ";
            movie_type = "b";
            director = "b";
            description = "b";
            imdb_rate = 0;
            which_hall = "b";
            release_date = "b";
            poster = "db.jpg";
        }


        public string getWhichHall()
        {
            return which_hall;
        }
        public void setHall(string m)
        {
            which_hall = m;
        }

        public string toString()
        {
            return movie_id + ";" + movie_name + ";" + description + ";" + release_date + "; "
            + director + ";" + movie_type + ";" + imdb_rate + ";" + poster + ";" + which_hall;
        }
        //public string insertMovie()
        //{
        //    return "insert into movies values('" + movie_name + "','" + description + "', '" + release_date + "' , '" + director + "' , '" + movie_type + "' , '" + imdb_rate + "')";
        //}
        //buraya tostring metodu yaz
    }
}
