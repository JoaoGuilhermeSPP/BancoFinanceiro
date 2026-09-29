using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoFinanceiro.Models;

namespace BancoFinanceiro.Service
{
   public class CadastraCliente
    {
        public Cliente CadastrarCliente(string nome, string cpf)
        {
            return new Cliente
            {
                Nome = nome,
                Cpf = cpf
            };
        }
        public Cliente ExcluirCliente(Cliente cliente)
        {
            return null;
        }

        public Cliente vincularConta(Cliente cliente, ContaCorrente conta)
        {
            cliente = new Cliente
            {
                Nome = cliente.Nome,
                Cpf = cliente.Cpf
            };
            conta.Titular = cliente;
            return cliente;
        }
      
    }
    
    
    public class BancoService
    {

        public ContaCorrente CriarContaCorrente(int numero, decimal saldo, Cliente titular, decimal limite, decimal taxa)
        {
            return new ContaCorrente
            {
                Numero = numero,
                Saldo = saldo,
                Titular = titular,
                Limite = limite,
                Taxa = taxa
            };
        }
       
        public decimal Depositar(decimal saldo, decimal valor, ContaCorrente conta)
        {
            
            conta.Saldo = saldo + valor;
            return conta.Saldo;
        }

        public decimal Sacar(decimal saldo, decimal valor, ContaCorrente conta)
        {

            if(valor > conta.Saldo)
            {
                throw new Exception("Saldo insuficiente");
            }
            else
            {
                conta.Saldo = saldo - valor;
                return conta.Saldo;
            }
        }
        public decimal VerificarSaldo(decimal saldo, ContaCorrente conta)
        {
            return conta.Saldo;
        }

        public static void ListarContas(ContaCorrente[] contas)
        {
            Console.WriteLine("Listando contas...");
            foreach (var conta in contas)
            {
                Console.WriteLine($"Conta: {conta.Numero},  Titular: {conta.Titular.Nome}");
            }

        }
}
}