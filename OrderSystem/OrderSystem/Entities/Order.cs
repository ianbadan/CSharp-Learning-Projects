using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OrderSystem.Entities.Enums;

namespace OrderSystem.Entities
{
    class Order
    {
        public DateTime Moment { get; set; }
        public OrderStatus Status { get; set; }
        public Client Client { get; set; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();

        public Order()
        {

        }
        public Order(DateTime moment, OrderStatus status, Client client)
        {
            Moment = moment;
            Status = status;
            Client = client;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
        }

        public double Total()
        {
            double sum = 0;
            foreach(OrderItem item in Items)
            {
                sum = sum + item.SubTotal();
            }
            return sum;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("ORDER SUMMARY:");
            text.AppendLine("Order moment: " + Moment.ToString("dd/MM/yyyy HH:mm:ss"));
            text.AppendLine("Order status: " + Status);
            text.AppendLine("Client: " + Client.ToString());
            text.AppendLine("Order Items: ");
            foreach(OrderItem item in Items)
            {
                text.AppendLine(item.ToString());
            }
            text.AppendLine("Total Price: $" + Total().ToString("F2", CultureInfo.InvariantCulture));

            return text.ToString();
                
        }
    }
}
