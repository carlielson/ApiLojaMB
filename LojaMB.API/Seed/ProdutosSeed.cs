using LojaMB.API.Context;
using LojaMB.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaMB.API.Seed
{
    public class ProdutosSeed
    {
        public static void Initialize(IServiceProvider service)
        {
            var scope = service.CreateScope();
            using (var context = new ApiContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                if (context.Produtos.Any())
                {
                    return;
                }

                context.Produtos.AddRange(
                    new Produto 
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "blusafem.jpg",
                        Nome = "Blusa Feminina",
                        Preco = 40.0
                    }, 
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "blusamas.jpg",
                        Nome = "Blusa Masculina",
                        Preco = 55.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "calcaja.jpg",
                        Nome = "Calça Jeans Azul - Masc.",
                        Preco = 150.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "calcajaf.jpg",
                        Nome = "Calça Jeans Azul - Fem.",
                        Preco = 180.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "calcajp.jpg",
                        Nome = "Calça Jeans Preta - Masc.",
                        Preco = 100.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "calcajpf.jpg",
                        Nome = "Calça Jeans Preta - Fem.",
                        Preco = 100.0
                    },
                     new Produto
                     {
                         Id = Guid.NewGuid(),
                         Imagem = "cueca.jpg",
                         Nome = "Cuecas - 3 un.",
                         Preco = 35.0
                     },
                     new Produto
                     {
                         Id = Guid.NewGuid(),
                         Imagem = "shortam.jpg",
                         Nome = "Short Azul - Masc.",
                         Preco = 35.0
                     },
                     new Produto
                     {
                         Id = Guid.NewGuid(),
                         Imagem = "shortfem.jpg",
                         Nome = "Short - Fem.",
                         Preco = 35.0
                     }
                    );
                context.SaveChanges();
            }
        }
    }
}
