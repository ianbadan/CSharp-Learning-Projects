using System;
using System.Collections.Generic;
using System.Globalization;
using TaxPayerSystem.Entities;

namespace TaxPayerSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TaxPayer> list = new List<TaxPayer>();

            Console.Write("Enter the number of tax payers: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Tax payer #{i+1} data:");
                Console.Write("Individual or company (i/c)? ");
                char option = char.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Anual income: ");
                double income = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                if(option == 'i')
                {
                    Console.Write("Health expenditures: ");
                    double healthExpenditures = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    list.Add(new Individual(name, income, healthExpenditures));
                }
                else if (option == 'c')
                {
                    Console.Write("Number of employees: ");
                    int numEmployees = int.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    list.Add(new Company(name, income, numEmployees));
                }
            }

            Console.WriteLine("\nTAXES PAID");
            double taxesSum = 0;
            foreach(TaxPayer tp in list)
            {
                Console.WriteLine(tp.Name + ": $" + tp.Tax().ToString("F2", CultureInfo.InvariantCulture));
                taxesSum += tp.Tax();
            }

            Console.WriteLine("\nTOTAL TAXES: $" + taxesSum.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
