using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AuctionHouse
{
    internal class MainMenu : Client
    {
        private static String _Mainoptions = /*private string as to not be modified*/
            "\nMain Menu\n" +
            "---------\n" +
            "(1) Register\n" +
            "(2) Sign In\n" +
            "(3) Exit\n" +
            "\nPlease select an option between 1 and 3";

        public string MainOptions
        {
            get { return _Mainoptions; }
        }

        private static String _logo = /*private string as to not be modified*/
            "+------------------------------+" +
            "\n| Welcome to the Auction House |" +
            "\n+------------------------------+" ;

        public string logo
        {
            get { return _logo; }
        }

        private static String _exitmessage = /*private string as to not be modified*/
            "+--------------------------------------------------+" +
            "\n| Good bye, thank you for using the Auction House! |" +
            "\n+--------------------------------------------------+";

        public string exitmessage
        {
            get { return _exitmessage; }
        }

        private static String _password_dialog = /*private string as to not be modified*/
        "\nPlease choose a password\r\n* At least 8 characters\r\n* No white space characters\r\n* At least one upper-case letter\r\n* At least one lower-case letter\r\n* At least one digit\r\n* At least one special character";

        public string password_dialog
        {
            get { return _password_dialog; }
        }

        private static String _loginmessage = "\nSign In\n-------\n\nPlease enter your email address";
        public string loginmessage
        {
            get { return _loginmessage; }
        }

        private static String _passwordmessage = "\nPlease enter your password";
        public string passwordmessage
        {
            get { return _passwordmessage; }
        }

        private static String _invalidlogin = "\nInvalid login details. ";
        public string invalidlogin
        {
            get { return _invalidlogin; }
        }

        private static String _invalidselection = "\nPlease enter a valid Selection.";
        public string invalidselection
        {
            get { return _invalidselection; }
        }

        private static String _invalidnumber = "\nPlease enter a Number.";
        public string invalidnumber
        {
            get { return _invalidnumber; }
        }

        public virtual void DisplayMenu() /*Displays the main menu and its functionality and error handling*/
        {
            bool runProgram = true;
            MainMenu mainMenu = new MainMenu();
            while (runProgram)
            {
                Console.WriteLine(mainMenu.MainOptions);
                int inputInt;
                Console.Write("> ");
                bool validInput = Int32.TryParse(Console.ReadLine(), out inputInt);
                if (validInput)
                {
                    if (inputInt == 1)
                    {
                        Registration registration = new Registration();
                        registration.Registration_Dialog(); //registration page
                    }
                    else if (inputInt == 2)
                    {
                        LogIn(); //login dialog
                    }
                    else if (inputInt == 3)
                    {
                        Console.WriteLine(mainMenu.exitmessage);
                        Environment.Exit(0); //Exit on 3 being registered
                    }
                    else
                    {
                        Console.WriteLine(invalidselection);
                    }
                }
                else
                {
                    Console.WriteLine(invalidnumber);
                }
            }
        }

        public void LogIn()
        {
            MainMenu mainMenu = new MainMenu();
            Console.WriteLine(mainMenu.loginmessage);
            Console.Write("> ");
            string emailinput = Console.ReadLine();
            Console.WriteLine(mainMenu.passwordmessage);
            Console.Write("> ");
            string passwordinput = Console.ReadLine();
            if (File.ReadAllText(@"Client.txt").Contains(emailinput + "<=seperator=>\n" + passwordinput))
            {
                string contents = File.ReadAllText(@"Client.txt");
                var start = contents.IndexOf(emailinput);
                var match2 = contents.Substring(start, contents.IndexOf("<=seperatorend=>"+emailinput) - start);
                string[] result = match2.Split("<=seperator=>");
                SetName(result[2]);
                SetPassword(passwordinput);
                SetEmail(emailinput);
                if (result[4].Contains("none"))
                {
                    Address validationaddress = new Address();
                    Console.WriteLine("Personal Details for " + result[2].Trim() + "(" + result[0] + ")\r\n------------------------------------------------------------------------------\n");
                    validationaddress.Address_Dialog();
                }
                else
                {
                    MainMenu clientmenu = new ClientMenu();
                    clientmenu.DisplayMenu();
                }

            }
            else 
            {
                Console.WriteLine(invalidlogin);
                return;
            }

        }

    }
}
