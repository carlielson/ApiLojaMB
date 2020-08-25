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
            // var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
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
                        Imagem = "notebook.jpg",
                        Nome = "Notebook",
                        Preco = 3000.0
                    }, 
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "tv.jpg",
                        Nome = "TV 30 POL",
                        Preco = 3000.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "nitendo.jpg",
                        Nome = "Nitendo Switch",
                        Preco = 3000.0
                    },
                    new Produto
                    {
                        Id = Guid.NewGuid(),
                        Imagem = "jbl.jpg",
                        Nome = "Jbl",
                        Preco = 1800.0
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
