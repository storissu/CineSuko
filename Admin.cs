using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineSuko
{
    public class Admin
    {
        int admin_id;
        string firstname;
        string lastname;
        string email;
        string username;
        string password;

        public Admin(int admin_id, string firstname, string lastname, string email, string username,
            string password)
        {
            this.admin_id = admin_id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.username = username;
            this.password = password;
        
        }
        public String getFirstname()
        {
            return firstname;
        }

        public String getLastname()
        {
            return lastname;
        }

        public String getUsername()
        {
            return username;
        }

        public String getPassword()
        {
            return password;
        }

        

    }
}
