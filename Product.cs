using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse;

namespace AuctionHouse
{
    internal class Product : Client //inherits from Client class to use client details
    {
        public void Product_Dialog() /*dialog and validation for product*/
        {
            Console.WriteLine("Product Advertisement for " + GetName().Trim() + "(" + GetEmail() + ")\r\n------------------------------------------------------------------------------\n");
            bool checker = true;
            while (checker) //error handling while checker
            {
                Console.WriteLine("Product name");
                Console.Write("> ");
                string productname = Console.ReadLine();
                bool flaggedname = string.IsNullOrEmpty(productname); /*flags name if empty*/
                if (flaggedname)
                {
                    Console.WriteLine("\nProduct name cannot be blank");
                }
                else
                {
                    while (checker)
                    {
                        Console.WriteLine("\nProduct Description");
                        Console.Write("> ");
                        string productdescription = Console.ReadLine();
                        if (string.IsNullOrEmpty(productdescription)) 
                        {
                            Console.WriteLine("\nProduct name cannot be blank. \n");
                        }
                        else if (productdescription == productname) //duplicate product description/name message
                        {
                            Console.WriteLine("\nProduct description cannot be same as product name. ");
                        }
                        else
                        {
                            while (checker)
                            {
                                Console.WriteLine("\nProduct price ($d.cc)");
                                Console.Write("> ");
                                string priceinput = Console.ReadLine();
                                if (string.IsNullOrEmpty(priceinput) || priceinput.Any(c => char.IsLetter(c)) || priceinput.Contains(".") == false || priceinput.StartsWith("$") == false) //invalid currency flags, i.e, doesn't start with dollar sign or has letters
                                {
                                    Console.WriteLine("\nInvalid Input: A currency value is requred, e.g. $54.95, $9.99, $2314.15.");
                                }
                                else
                                {
                                    string[] cents = priceinput.Split("."); //splits from the dot
                                    if (cents[1].Count() > 2) //ensures the cents only has 2 values
                                    { 
                                        Console.WriteLine("\nInvalid Input: A currency value is requred, e.g. $54.95, $9.99, $2314.15."); 
                                    }
                                    else
                                    {
                                        using StreamWriter writer = new StreamWriter(@"Product.txt", true); //writes to product text file
                                        Console.WriteLine($"\nSuccessfully added product {productname}, {productdescription}, {priceinput}.");
                                        writer.WriteLine($"{GetEmail()}<=seperator=>{productname}<=seperator=>{productdescription}<=seperator=>{priceinput}<=seperator=>\n<=midseperator=>"); //seperatores for splitting in the future
                                        return;
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
