using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaMB.API.Models
{
    public class Pedido
    {
        public Guid? Id { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
        public Guid? IdCliente { get; set; }
        public bool? Concluido { get; set; }
        public Guid? IdLote { get; set; }

        [NotMapped]
        public Produto Produto { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }

    }
}
