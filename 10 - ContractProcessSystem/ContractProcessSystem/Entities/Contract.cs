using System;
using System.Collections.Generic;
using System.Globalization;


namespace ContractProcessSystem.Entities
{
    class Contract
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public double TotalValue { get; set; }
        public List<Installment> Installments { get; private set; }

        public Contract(int number, DateTime date, double totalValue)
        {
            Number = number;
            Date = date;
            TotalValue = totalValue;
            Installments = new List<Installment>();
        }

        public void AddInstallment(Installment installment)
        {
            Installments.Add(installment);
        }

        public override string ToString()
        {
            string message = "Installments:\n";
            foreach(Installment i in Installments)
            {
                message += i.ToString();
            }
            return message;
        }
    }

}
