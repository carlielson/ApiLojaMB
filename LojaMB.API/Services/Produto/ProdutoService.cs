using LojaMB.API.Context;
using LojaMB.API.Models;
using LojaMB.API.Services.Inteface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaMB.API.Services
{
    public class ProdutoService: IProdutoService
    {
        public ApiContext _context { get; set; }
        public ProdutoService(ApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Guid> InsertPedidos(List<Pedido> pedidos)
        {
            Guid idLote = Guid.NewGuid();

            foreach (var item in pedidos)
            {
                item.IdLote = idLote;
                await InsertPedido(item);
            }

            return idLote;
        }

        private async Task InsertPedido(Pedido pedido)
        {
            pedido.Id = Guid.NewGuid();
            pedido.Concluido = false;
            await _context.Pedidos.AddAsync(pedido);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Pedido>> PedidosByIdLote(Guid idLote)
        {
            var pedidos = await _context.Pedidos.Where(x => x.IdLote == idLote).ToListAsync();

            foreach (var item in pedidos)
            {
                var produto = await GetById(item.IdProduto);

                if(produto != null)
                    item.Produto = produto;

                if (item.IdCliente != null) 
                {
                    var cliente = await GetCliente(item.IdCliente.Value);
                    item.Cliente = cliente;
                }
                
            }

            return pedidos;
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente> InsertCliente(Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            await _context.Clientes.AddAsync(cliente);
             _context.SaveChanges();

            return cliente;
        }

        public async Task UpdateLoteCliente(Guid idLote, Guid cliente)
        {
            var pedidos = _context.Pedidos.Where(x => x.IdLote == idLote).ToList();

            foreach (var item in pedidos)
            {
                item.IdCliente = cliente;
            }

            _context.Pedidos.UpdateRange(pedidos);

            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> GetCliente(Guid id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Pedido> UpdatePedido(Guid idPedido, Pedido pedido)
        {
            var pedidoDb = _context.Pedidos.FirstOrDefault(x => x.Id == idPedido);
            if (pedido != null) 
            {
                pedidoDb.Quantidade = pedido.Quantidade;
                await _context.SaveChangesAsync();                
            }
            return pedidoDb;
        }

        public async Task UpdatePedido(List<Pedido> pedidos)
        {
            foreach (var item in pedidos)
            {
                var pedidoDb = _context.Pedidos.FirstOrDefault(x => x.Id == item.Id);
                if (pedidoDb != null)
                {
                    if (item.Quantidade == 0)
                    {
                        _context.Remove(pedidoDb);
                        await _context.SaveChangesAsync();
                    }
                    else 
                    {
                        pedidoDb.Quantidade = item.Quantidade;
                        await _context.SaveChangesAsync();
                    }
                    
                }
            }
            
            // return pedidoDb;
        }

        public async Task<IEnumerable<Pedido>> ConcluirPedido(Guid idLote)
        {
            var pedidoDb = _context.Pedidos.Where(x => x.IdLote == idLote).ToList();
            if (pedidoDb.Any())
            {
                foreach (var item in pedidoDb)
                {
                    item.Concluido = true;
                    _context.Pedidos.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
            return await PedidosByIdLote(idLote);
        }

        public async Task DeletePedido(Guid id)
        {
            var pedidoDb = _context.Pedidos.FirstOrDefault(x => x.Id == id);
            if (pedidoDb != null)
            {
                _context.Pedidos.Remove(pedidoDb);
                await _context.SaveChangesAsync();
            }
        }
    }
}
