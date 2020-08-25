using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaMB.API.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Imagem { get; set; }
    }
}
