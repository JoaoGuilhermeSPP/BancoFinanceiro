using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoFinanceiro.Models
{
    public abstract class Conta
    {
        public  int Numero { get; set; }
        public   decimal Saldo { get; set; }
        public   Cliente Titular { get; set; }
    }
}