using System;

namespace SistemaBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            int codigo;
            String nome;
            decimal valor;
            char hasDeposito;
            ContaBancaria c1;


            Console.Write("Informe o codigo da conta: ");
            codigo = int.Parse(Console.ReadLine());
            Console.Write("Entre com o nome do titular da conta: ");
            nome = Console.ReadLine();
            Console.Write("Haverá deposito inicial? (s/n): ");
            hasDeposito = char.Parse(Console.ReadLine());
            if (hasDeposito == 'S' || hasDeposito == 's')
            {
                Console.Write("Entre o valor do depósito inicial: ");
                valor = decimal.Parse(Console.ReadLine());
                c1 = new ContaBancaria(codigo, nome, valor);
            }
            else
            {
                c1 = new ContaBancaria(codigo, nome);
            }

            Console.WriteLine("\nDados da conta \n" + c1);

            Console.Write("Entre com o valor para depósito: ");
            valor = decimal.Parse(Console.ReadLine());
            c1.Deposito(valor);
            Console.WriteLine("\nDados da conta atualizados:\n" + c1);

            Console.Write("Entre com o valor para saque: ");
            valor = decimal.Parse(Console.ReadLine());
            c1.Saque(valor);
            Console.WriteLine("\nDados da conta atualizados:\n" + c1);
        }
    }
}

