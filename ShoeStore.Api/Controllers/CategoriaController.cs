using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.Categoria.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaApplication _categoriaApplication;

        public CategoriaController(ICategoriaApplication categoriaApplication)
        {
            _categoriaApplication = categoriaApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListCategorias([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _categoriaApplication.ListCategorias(filters);

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCategorias()
        {
            var response = await _categoriaApplication.ListSelectCategorias();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CategoriaById(int id)
        {
            var response = await _categoriaApplication.CategoriaById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategoria([FromBody] CategoriaRequestDto request)
        {
            var response = await _categoriaApplication.RegisterCategoria(request);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditCategoria(int id, [FromBody] CategoriaRequestDto request)
        {
            var response = await _categoriaApplication.EditCategoria(id, request);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveCategoria(int id)
        {
            var response = await _categoriaApplication.RemoveCategoria(id);

            return Ok(response);
        }
    }
}
