using System;
using System.Globalization;
using OrderSystem.Entities;
using OrderSystem.Entities.Enums;

namespace OrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter client data:");
            Console.Write("Name: ");
            string clientName = Console.ReadLine();
            Console.Write("Email: ");
            string clientEmail = Console.ReadLine();
            Console.Write("Birth date (DD/MM/YYYY): ");
            DateTime clientBirth = DateTime.Parse(Console.ReadLine());
            Client client = new Client(clientName, clientEmail, clientBirth);
            Console.WriteLine("Enter order data:");
            Console.Write("Status: ");
            OrderStatus orderStatus = Enum.Parse<OrderStatus>(Console.ReadLine());
            Order order = new Order(DateTime.Now, orderStatus, client);
            Console.Write("How many items to this order? ");
            int n = int.Parse(Console.ReadLine());
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter #{i+1} item data:");
                Console.Write("Product name: ");
                string productName = Console.ReadLine();
                Console.Write("Product price: ");
                double productPrice = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Quantity: ");
                int productQuantity = int.Parse(Console.ReadLine());

                Product product = new Product(productName, productPrice);
                OrderItem item = new OrderItem(productQuantity, productPrice, product);
                order.AddItem(item);
            }

            Console.WriteLine(order);


        }
    }
}
