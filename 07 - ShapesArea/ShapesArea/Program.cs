using System;
using System.Collections.Generic;
using System.Globalization;
using ShapesArea.Entities;
using ShapesArea.Entities.Enums;

namespace ShapesArea
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> list = new List<Shape>();

            Console.Write("Enter the number of shapes: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Shape #{i + 1} data:");
                Console.Write("Rectangle or Circle (c/r)? ");
                char option = char.Parse(Console.ReadLine());
                Console.Write("Color (Black/Blue/Red): ");
                Color color = Enum.Parse<Color>(Console.ReadLine());
                if (option == 'r' || option == 'R')
                {
                    Console.Write("Widht: ");
                    double widht = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    Console.Write("Height: ");
                    double height = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    list.Add(new Rectangle(widht, height, color));
                }
                else if (option == 'c' || option == 'C')
                {
                    Console.Write("Radius: ");
                    double radius = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    list.Add(new Circle(radius, color));
                }
            }

            Console.WriteLine("\nSHAPE AREAS:");
            foreach (Shape shape in list)
            {
                Console.WriteLine(shape.GetType().Name + ": " + shape.Area().ToString("F2", CultureInfo.InvariantCulture));
            }

        }
    }
}
