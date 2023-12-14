using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.Producto.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoApplication _productoApplication;

        public ProductoController(IProductoApplication productoApplication)
        {
            _productoApplication = productoApplication;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListProductos([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _productoApplication.ListProductos(filters);
            return Ok(response);
        }

        [HttpGet("Usuario/{userId:int}")]
        public async Task<IActionResult> ListProductosByUser([FromQuery] BaseFiltersRequest filters, int userId)
        {
            var response = await _productoApplication.ListProductosByUser(filters, userId);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ProductoById(int id)
        {
            var response = await _productoApplication.ProductoById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProducto([FromForm] ProductoRequestDto request)
        {
            var response = await _productoApplication.RegisterProducto(request);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditProducto(int id, [FromForm] ProductoRequestDto request)
        {
            var response = await _productoApplication.EditProducto(id, request);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveProducto(int id)
        {
            var response = await _productoApplication.RemoveProducto(id);

            return Ok(response);
        }
    }
}
