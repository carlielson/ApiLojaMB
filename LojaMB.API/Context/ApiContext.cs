using LojaMB.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaMB.API.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Cliente>()
               .HasKey(p => p.Id);
        }
    }
}
