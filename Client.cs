using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    public class Client
    {
        private static string _name; //String is set to private
        private static string _email;
        private static string _password;

        public static string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public static string Email
        {
            get { return _email; } 
            set { _email = value; }
        }

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        public void SetName (string name) //Allows name to be set
        {
            _name = name;
        }

        public void SetEmail(string Email)
        {
            _email = Email;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public static string GetName() //allows other classes to access set name
            {
            return Name;
            }

        public static string GetEmail()
        {
            return Email;
        }

        public static string GetPassword()
        {
            return Password;
        }


    }
}
