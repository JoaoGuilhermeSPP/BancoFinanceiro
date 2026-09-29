using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoFinanceiro.Models;
using BancoFinanceiro.Service;
using System.IO;
using System.Text.Json;

namespace BancoFinanceiro.Data
{
    public class SalvarDados
    {
    
      
        public void SalvarContas(ContaCorrente[] contas)
        {
            string json = JsonSerializer.Serialize(contas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("contas.json", json);
        }   

        public void GerarArquivoJSON(ContaCorrente[] contas)
        {
            Console.WriteLine("Gerando arquivo JSON...");
            
            SalvarContas(contas);
        }   
        public List<ContaCorrente> CarregarContas()
        {
             if (File.Exists("contas.json"))
            {
                string json = File.ReadAllText("contas.json");
                
                return JsonSerializer.Deserialize<List<ContaCorrente>>(json) ?? new List<ContaCorrente>();
            }

            return new List<ContaCorrente>();
        }  
    }
}