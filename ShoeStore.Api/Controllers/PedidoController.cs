using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.Pedido.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoApplication _pedidoApplication;

        public PedidoController(IPedidoApplication pedidoApplication)
        {
            _pedidoApplication = pedidoApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListPedidos([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _pedidoApplication.ListPedidos(filters);

            return Ok(response);
        }

        [HttpGet("Usuario/{userId:int}")]
        public async Task<IActionResult> ListPedidoByUser(int userId)
        {
            var response = await _pedidoApplication.ListPedidosByUser(userId);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> PedidoById(int id)
        {
            var response = await _pedidoApplication.PedidoById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPedido([FromBody] PedidoRequestDto requestDto)
        {
            var response = await _pedidoApplication.RegisterPedido(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditPedido(int id, [FromBody] PedidoRequestDto requestDto)
        {
            var response = await _pedidoApplication.EditPedido(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemovePedido(int id)
        {
            var response = await _pedidoApplication.RemovePedido(id);

            return Ok(response);
        }

        [HttpPut("RemoveAll/{userId:int}")]
        public async Task<IActionResult> RemoveAllPedido(int userId)
        {
            var response = await _pedidoApplication.RemoveAllPedidos(userId);

            return Ok(response);
        }
    }
}
