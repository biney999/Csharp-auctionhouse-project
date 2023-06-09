using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    internal class ClientMenu : MainMenu //Inherits from main menu
    {

        private static String _Clientoptions =
            "\nClient Menu\n" +
            "-----------\n" +
            "(1) Advertise Product\r\n(2) View My Product List\r\n(3) Search For Advertised Products\r\n(4) View Bids On My Products\r\n(5) View My Purchased Items\r\n(6) Log off\r\n";

        public string ClientOptions
        {
            get { return _Clientoptions; }
        }

        public override void DisplayMenu()
        {
            bool runProgram = true;
            ClientMenu clientMenu = new ClientMenu();
            while (runProgram)
            {
                Console.WriteLine(clientMenu.ClientOptions);
                int inputInt;
                Console.Write("> ");
                bool validInput = Int32.TryParse(Console.ReadLine(), out inputInt);
                if (validInput)
                {
                    if (inputInt == 1)
                    {
                        Product newclient = new Product();
                        newclient.Product_Dialog();
                    }
                    else if (inputInt == 2)
                    {

                    }
                    else if (inputInt == 6)
                    {
                        MainMenu displaymenu = new MainMenu();
                        displaymenu.DisplayMenu();
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
    }
}
