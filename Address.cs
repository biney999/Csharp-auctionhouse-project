using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AuctionHouse
{
    internal class Address : Client
    {
        private static String _unitnumber = "Unit number (0 = none):"; //all the text is stored privately
        private static String _streetnumber = "\nStreet number:";
        private static String _streetnumbermessage = "street number cannot be 0: ";
        private static String _streetname = "\nStreet name:";
        private static String _streetnamemessage = "Street name cannot be blank. ";
        private static String _streetsuffix = "\nStreet suffix:";
        private static String _stsuffixmessage = "Street suffix cannot be blank or contain numbers.";
        private static String _cityname = "\nCity:";
        private static String _cityerror = "City cannot be blank.";
        private static String _states = "\nState (ACT, NSW, NT, QLD, SA, TAS, VIC, WA)";
        private static String _invalidstates = "Please provide a valid state.";
        private static String _postcodes = "\nPostcode (1000 .. 9999):";
        private static String _invalidpostcodes = "Please provide a valid postcode";
        private static String _unit_error = "Unit number must be a non-negative integer.";

        public string unitnumber
        {
            get { return _unitnumber; }
        }

        public string streetnumber
        {
            get { return _streetnumber; }
        }

        public string streetnumbermessage
        {
            get { return _streetnumbermessage; }
        }

        public string streetname
        {
            get { return _streetname; }
        }

        public string streetnamemessage
        {
            get { return _streetnamemessage; }
        }

        public string Suffix_street
        {
            get { return _streetsuffix; }
        }

        public string stsuffixmessage
        {
            get { return _stsuffixmessage; }
        }

        public string cityname
        {
            get { return _cityname; }
        }

        public string cityerror
        {
            get { return _cityerror; }
        }

        public string states
        {
            get { return _states; }
        }

        public string invalidstates
        {
            get { return _invalidstates; }
        }

        public string postcodes
        {
            get { return _postcodes; }
        }

        public string invalidpostcodes
        {
            get { return _invalidpostcodes; }
        }

        public string unit_error
        {
            get { return _unit_error; }
        }


        public void Address_Dialog()
        {
            bool checker = true;
            bool programrunner = true;
            while (programrunner)
            {
                while (checker)
                {
                    uint inputInt;
                    Console.WriteLine(unitnumber);
                    Console.Write("> ");
                    bool validInput = UInt32.TryParse(Console.ReadLine(), out inputInt); //only allows positive integers
                    if (validInput)
                    {
                        while (checker)
                        {
                            Console.WriteLine(streetnumber);
                            Console.Write("> ");
                            uint streetInt;
                            bool streetInput = UInt32.TryParse(Console.ReadLine(), out streetInt);
                            if (streetInt == 0)
                            {
                                Console.WriteLine(streetnumbermessage);
                            }
                            else
                            {
                                while (checker)
                                {
                                    Console.WriteLine(streetname);
                                    Console.Write("> ");
                                    string streetnameinput = Console.ReadLine();
                                    bool flaggedstreetname = string.IsNullOrEmpty(streetnameinput);
                                    if (flaggedstreetname == true)
                                    {
                                        Console.WriteLine(streetnamemessage);
                                    }
                                    else
                                    {
                                        while (checker)
                                        {
                                            Console.WriteLine(Suffix_street);
                                            Console.Write("> ");
                                            string streetsuffixinput = Console.ReadLine();
                                            bool streetsuffix = string.IsNullOrEmpty(streetsuffixinput) || streetsuffixinput.Any(c => char.IsDigit(c));
                                            if (streetsuffix == true)
                                            {
                                                Console.WriteLine(stsuffixmessage);
                                            }
                                            else
                                            {
                                                while (checker)
                                                {
                                                    Console.WriteLine(cityname);
                                                    Console.Write("> ");
                                                    string cityinput = Console.ReadLine();
                                                    bool cityverification = string.IsNullOrEmpty(cityinput) || cityinput.Any(c => char.IsLetter(c)) == false; 
                                                    if (cityverification == true)
                                                    {
                                                        Console.WriteLine(cityerror);
                                                    }
                                                    else
                                                    {
                                                        while (checker)
                                                        {
                                                            Console.WriteLine(states);
                                                            Console.Write("> ");
                                                            string stateinput = Console.ReadLine();
                                                            string stateupper = stateinput.ToUpper(); //sets the input to uppercase in order to match with states
                                                            string[] validstates = { "ACT", "NSW", "NT", "QLD", "SA", "TAS", "VIC", "WA" };
                                                            if (validstates.Contains(stateupper) == false) // if it doesn't matchl;
                                                            {
                                                                Console.WriteLine(invalidstates);
                                                            }
                                                            else
                                                            {
                                                                while (checker)
                                                                {
                                                                    Console.WriteLine(postcodes);
                                                                    Console.Write("> ");
                                                                    uint postcodeInt;
                                                                    bool postcodeInput = UInt32.TryParse(Console.ReadLine(), out postcodeInt); //only allowws positive intergers to be input
                                                                    if (postcodeInt < 1000 || postcodeInt > 9999) //sets postcode requirment 
                                                                    {
                                                                        Console.WriteLine(invalidpostcodes);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (inputInt == 0)
                                                                        { // if unit number is 0, it is not displayed
                                                                            Console.WriteLine($"Address has been updated to {streetInt} {streetnameinput} {streetsuffixinput}, {cityinput} {stateupper} {postcodeInt}");
                                                                        }
                                                                        else
                                                                        { //unit number displayed
                                                                            Console.WriteLine($"Address has been updated to {inputInt}/{streetInt} {streetnameinput} {streetsuffixinput}, {cityinput} {stateupper} {postcodeInt}");
                                                                        }
                                                                        string contents = File.ReadAllText(@"Client.txt"); //opens Client file
                                                                        var indexA = GetName()+ "<=seperator=>\naddress:"; //uses name stored in client as the primary key to find address
                                                                        var start = contents.IndexOf(indexA); //indexes position of name and address
                                                                        var match2 = contents.Substring(start, contents.IndexOf("<=seperatorend=>" + GetEmail()) - start); //sets everything between name and ending seperator as substring
                                                                        string[] result = match2.Split("<=seperator=>"); //splits the string through the seperator
                                                                        string addresswriter = ($"\n{inputInt}\n{streetInt}\n{streetnameinput}\n{streetsuffixinput}\n{cityinput}\n{stateupper}\n{postcodeInt}<=seperatorend=>{GetEmail()}"); //prepares string to be written to file
                                                                        var replacement = contents.Replace(result[2] + "<=seperatorend=>" + GetEmail(), addresswriter); 
                                                                        File.WriteAllText("Client.txt", replacement); //replaces null address field with new address
                                                                        checker = false; //breaks out of the nested while loop to return to menu
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(unit_error);
                    }
                }
                MainMenu clientmenu = new ClientMenu(); //returns to main menu
                clientmenu.DisplayMenu();
            }
        }
    }

}
