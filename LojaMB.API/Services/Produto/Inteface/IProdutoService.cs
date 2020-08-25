using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LojaMB.API.Models;
namespace LojaMB.API.Services.Inteface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);
        Task<IEnumerable<Pedido>> PedidosByIdLote(Guid idLote);
        Task<Guid> InsertPedidos(List<Pedido> pedidos);
        Task<Cliente> InsertCliente(Cliente cliente);
        Task<Cliente> GetCliente(Guid id);
        Task UpdateLoteCliente(Guid idLote, Guid cliente);
        Task<Pedido> UpdatePedido(Guid idPedido, Pedido pedido);
        Task<IEnumerable<Pedido>> ConcluirPedido(Guid idLote);
        Task DeletePedido(Guid id);
        Task UpdatePedido(List<Pedido> pedidos);

    }
}
