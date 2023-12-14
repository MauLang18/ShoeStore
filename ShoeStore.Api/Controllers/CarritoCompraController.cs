using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.CarritoCompra.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {
        private readonly ICarritoCompraApplication _carritoCompraApplication;

        public CarritoCompraController(ICarritoCompraApplication carritoCompraApplication)
        {
            _carritoCompraApplication = carritoCompraApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListCarritoCompra([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _carritoCompraApplication.ListCarritoCompra(filters);

            return Ok(response);
        }

        [HttpGet("Usuario/{userId:int}")]
        public async Task<IActionResult> ListCarritoCompraByUser(int userId)
        {
            var response = await _carritoCompraApplication.ListCarritoCompraByUser(userId);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CarritoCompraById(int id)
        {
            var response = await _carritoCompraApplication.CarritoCompraById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCarritoCompra([FromBody] CarritoCompraRequestDto requestDto)
        {
            var response = await _carritoCompraApplication.RegisterCarritoCompra(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditCarritoCompra(int id, [FromBody] CarritoCompraRequestDto requestDto)
        {
            var response = await _carritoCompraApplication.EditCarritoCompra(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveCarritoCompra(int id)
        {
            var response = await _carritoCompraApplication.RemoveCarritoCompra(id);

            return Ok(response);
        }

        [HttpPut("RemoveAll/{userId:int}")]
        public async Task<IActionResult> RemoveAllCarritoCompra(int userId)
        {
            var response = await _carritoCompraApplication.RemoveAllCarritoCompra(userId);

            return Ok(response);
        }
    }
}
