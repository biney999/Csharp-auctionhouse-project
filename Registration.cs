using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse;

namespace AuctionHouse
{
    internal class Registration : MainMenu
    {
        //Private strings containg messages, as to not be modified
        private static String _emailerror = "\nThe supplied value is not a valid email address.";
        private static String _entername = "\nPlease enter your name";
        private static String _nullname = "\nName cannot be blank or contain numbers and special characters";
        private static String _enteremail = "\nPlease enter your email address";
        private static String _emailduplicate = "The supplied address is already in use. ";
        private static String _invalidpassword = "\nThe supplied value is not a valid password.";



        //Return for strings
        public static string emailError
        {
            get { return _emailerror; }
        }
        public static string entername
        {
            get { return _entername; }
        }

        public static string nullname
        {
            get { return _nullname; }
        }
        public static string enteremail
        {
            get { return _enteremail; }
        }
        public static string emailduplicate
        {
            get { return _emailduplicate; }
        }
        public static string invalidpassword
        {
            get { return _invalidpassword; }
        }


        public void Registration_Dialog() /*Dialog and validation for registration*/
        {
            MainMenu mainmenu = new MainMenu();
            bool checker = true; //Bool for while loop to exception handle
            while (checker)
            {
                Console.WriteLine(entername); //name dialog
                Console.Write("> ");
                string inputname = Console.ReadLine();
                bool flaggedname = string.IsNullOrEmpty(inputname) || inputname.Any(c => char.IsDigit(c)) || inputname.Any(c => char.IsSymbol(c)); /*Flagged characters, if found true will display error message*/
                if (flaggedname)
                {
                    Console.WriteLine(nullname); //Message for invalid name
                }
                else
                {
                    while (checker)
                    {
                        Console.WriteLine(enteremail);
                        Console.Write("> ");
                        string inputemail = Console.ReadLine();
                        if (inputemail.StartsWith("@") || inputemail.EndsWith("@") || string.IsNullOrEmpty(inputemail)) //seperate if-statement due to the '@' confusing the split string below, also requires input to not be null
                        {
                            Console.WriteLine(emailError);
                        }
                        else if (File.ReadAllText(@"Client.txt").Contains(inputemail)) /*Makes sure emails aren't duplicate as specified in user story*/
                        {
                            Console.WriteLine(emailduplicate);
                        }
                        else if (inputemail.Contains(".") && inputemail.Contains("@")) /*checks if stirng contains @ and . symbol*/
                        {
                            string[] emailsplitter = inputemail.Split('@', 2); /*splits email into 2 parts to manually search prefix and suffix for requirments*/
                            bool prefixflagsfalse = emailsplitter[0].EndsWith(".") || emailsplitter[0].EndsWith("_") || emailsplitter[0].EndsWith("-") || emailsplitter[1].EndsWith(".") || emailsplitter[1].StartsWith("."); /*flags if found will cause error message*/
                            bool prefixflagstrue = emailsplitter[0].All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '.') || emailsplitter[1].All(c => char.IsLetterOrDigit(c) || c == '-' || c == '.'); /*allows only letters and numbers and allowed symbols*/
                            if (prefixflagsfalse == true || prefixflagstrue == false)
                            {
                                Console.WriteLine(emailError); //displays error if flagged characters are found
                            }
                            else
                            {
                                while (checker)
                                {
                                    Console.WriteLine(mainmenu.password_dialog);
                                    Console.Write("> ");
                                    string inputpassword = Console.ReadLine();
                                    bool flaggedpword = inputpassword.Count(c => !Char.IsWhiteSpace(c)) < 8; /*length of password without whitespace*/
                                    bool pwordrequirments = inputpassword.Any(c => char.IsDigit(c)) & inputpassword.Any(c => char.IsUpper(c)) & inputpassword.Any(c => char.IsLower(c)) & inputpassword.Any(c => char.IsSymbol(c) || c > 32 && c < 127); /*sets requirments, including which symbols to use (any printable ascii symbols)*/
                                    if (flaggedpword == false & pwordrequirments == true)
                                    {
                                        using StreamWriter writer = new StreamWriter(@"Client.txt", true);  /*creates the client data in the text file, as well as an empty address*/
                                        writer.WriteLine(inputemail + "<=seperator=>\n" + inputpassword + "<=seperator=>\n" + inputname + "<=seperator=>\n" + "address:<=seperator=>\nnone<=seperatorend=>" + inputemail + '\n'); //using these seperators as to easily split in the future
                                        Console.WriteLine("\nClient " + inputname + "(" + inputemail + ") has successfully registered at the Auction House.");
                                        return; //returns to main menu
                                    }
                                    else
                                    {
                                        Console.WriteLine(invalidpassword);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(emailError);
                        }
                    }
                }
            }
        }
    }
}
