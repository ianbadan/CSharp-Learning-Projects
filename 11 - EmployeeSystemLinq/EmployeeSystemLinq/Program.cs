using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EmployeeSystemLinq.Entities;

namespace EmployeeSystemLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> employees = new List<Employee>();
            try
            {
                using StreamReader sr = File.OpenText(path);
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                    employees.Add(new Employee(name, email, salary));
                }

                Console.Write("Enter Salary: ");
                double sal = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emails = employees.Where(e => e.Salary > sal).OrderBy(e => e.Email).Select(e => e.Email);

                Console.WriteLine("\nEmail of people whose salary is more than " + sal.ToString("F2", CultureInfo.InvariantCulture)+ ":");
                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }

                var sum = employees.Where(e => e.Name[0] == 'M').Select(e => e.Salary).Sum();

                Console.Write("\nSum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine();
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
