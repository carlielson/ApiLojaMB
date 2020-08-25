using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaMB.API.Models
{
    public class Cliente
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
