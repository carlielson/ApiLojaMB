using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaMB.API.Models;
using LojaMB.API.Services.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace LojaMB.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private IProdutoService _service;
        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        // GET: api/<ProdutosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPut("pedidos/{idPedido}")]
        public async Task<ActionResult<Guid>> PostPedidos(Guid idPedido,[FromBody] Pedido pedido)
        {
            if (idPedido != pedido.Id)
                return NotFound("Pedido não localizado");

            return Ok(await _service.UpdatePedido(idPedido, pedido));
        }

        [HttpPut("pedidos/atualizar/{idLote}")]
        public async Task<ActionResult<Guid>> UpdatePedidos(Guid idLote, [FromBody] List<Pedido> pedidos)
        {
            if (idLote != pedidos.FirstOrDefault().IdLote)
                return NotFound("Lote não localizado");

            await _service.UpdatePedido(pedidos);
            return Ok(true);
        }

        [HttpPost("pedidos")]
        public async Task<ActionResult<Guid>> PostPedidos([FromBody] List<Pedido> pedidos)
        {
            if (!pedidos.Any())
                return NotFound();

            return Ok(await _service.InsertPedidos(pedidos));
        }

        [HttpGet("pedido/lote/{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos(Guid id)
        {
            return Ok(await _service.PedidosByIdLote(id));
        }

        [HttpPost("cliente")]
        public async Task<ActionResult<Cliente>> Cliente([FromBody] Cliente cliente)
        {
            return Ok(await _service.InsertCliente(cliente));
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("pedidos/cliente/{idLote}")]
        public async Task<IActionResult> Put(Guid idLote, [FromBody] PedidoCliente pedidoCliente)
        {
            if (idLote != pedidoCliente.IdLote || pedidoCliente.IdCliente == Guid.Empty) 
            {
                return NotFound("Cliente ou lote não localizado");
            }
            var pedidos = await _service.PedidosByIdLote(idLote);
            var cliente = await _service.GetCliente(pedidoCliente.IdCliente);
            if (pedidos == null || cliente == null) 
            {
                return NotFound("Cliente ou lote não localizado");
            }               

            await _service.UpdateLoteCliente(idLote, pedidoCliente.IdCliente);
            return Ok(true);
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("pedido/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeletePedido(id);
            return Ok(true);
        }

        [HttpGet("concluir/pedido/{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> ConcluirPedidos(Guid id)
        {
            return Ok(await _service.ConcluirPedido(id));
        }
    }
}
