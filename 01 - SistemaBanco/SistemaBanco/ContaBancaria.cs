using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SistemaBanco
{
    class ContaBancaria
    {
        const int taxa = 5;
        public int Codigo { get; private set; }
        public string NomeTitular { get; set; }
        public decimal Saldo { get; private set; }

        public ContaBancaria(int codigo, string nomeTitular)
        {
            Codigo = codigo;
            NomeTitular = nomeTitular;
        }

        public ContaBancaria(int codigo, string nomeTitular, decimal depositoInicial) : this(codigo, nomeTitular)
        {
            Deposito(depositoInicial);
        }

        public void Deposito(decimal valor)
        {
            Saldo += valor;
        }

        public void Saque(decimal valor)
        {

            Saldo = Saldo - valor - taxa;

        }

        public override string ToString()
        {
            return "Conta " + Codigo + ", Titular: " + NomeTitular + ", Saldo: " + Saldo.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
