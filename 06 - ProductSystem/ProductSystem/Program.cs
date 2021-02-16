using System;
using System.Collections.Generic;
using System.Globalization;
using ProductSystem.Entities;

namespace ProductSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> list = new List<Product>();

            Console.Write("Enter the number of products: ");
            int n = int.Parse(Console.ReadLine());
            for(int i = 0; i < n; i++)
            {
                Console.Write("Common, used or imported (c/u/i)? ");
                char productType = char.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                switch (productType)
                {
                    case 'c':
                        list.Add(new Product(name, price));
                        break;
                    case 'u':
                        Console.Write("Manufacture date (DD/MM/YYYY): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        list.Add(new UsedProduct(name, price, date));
                        break;
                    case 'i':
                        Console.Write("Customs fee: ");
                        double fee = double.Parse(Console.ReadLine());
                        list.Add(new ImportedProduct(name, price, fee));
                        break;
                    default:
                        Console.WriteLine("Input out of the accepted values");
                        break;
                }      
            }

            Console.WriteLine();
            Console.WriteLine("PRICE TAG:");
            foreach(Product prod in list)
            {
                Console.WriteLine(prod.PriceTag());
            }
        }
    }
}
