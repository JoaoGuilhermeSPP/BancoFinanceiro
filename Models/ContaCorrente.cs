using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoFinanceiro.Models
{
    public class ContaCorrente : Conta
    {
        public decimal Limite { get; set; }
        public decimal Taxa { get; set; }
    }
       
}