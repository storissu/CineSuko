using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineSuko
{
    public class Customer
    {
        int id;
        string firstname;
        string lastname;
        string birthdate;
        int age;
        string phone;
        string email;
        string username;
        string password;
        string gender;
        string isStudent;

        public Customer(int id, string firstname, string lastname, string birthdate, string phone, string email, string username,
            string password, string gender, string isStudent)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.birthdate = birthdate;
            this.phone = phone;
            this.email = email;
            this.username = username;
            this.password = password;
            this.gender = gender;
            this.isStudent = isStudent;
        }
        public int getId()
        {
            return id;
        }
        public String getFirstname()
        {
            return firstname;
        }
        public String getLastname()
        {
            return lastname;
        }

        public String getEmail()
        {
            return email;
        }

        public String getUsername()
        {
            return username;
        }

        public String getPassword()
        {
            return password;


        }

        public String getGender()
        {
            return gender;


        }

        public String getIsStudent()
        {
            return isStudent;


        }


        public String toString()
        {
            return id + ";" + getFirstname() + ";" + getLastname() + ";"; // devam ettir yazdırırken
        }
    }
}
