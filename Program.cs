using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (!File.Exists("Client.txt"))
            {
                StreamWriter newfile = new StreamWriter("Client.txt");
                newfile.Close(); //creates new file if it does not exist
            }

            if (!File.Exists("Product.txt"))
            {
                StreamWriter newfile = new StreamWriter("Product.txt");
                newfile.Close(); //creates new file if it does not exist
            }
            MainMenu displaymenu = new MainMenu();
            Console.WriteLine(displaymenu.logo); //displays the logo on startup
            displaymenu.DisplayMenu();

        }
    }
}