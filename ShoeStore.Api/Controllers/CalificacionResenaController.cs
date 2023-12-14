using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.CalificacionResena.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionResenaController : ControllerBase
    {
        private readonly ICalificacionResenaApplication _calificacionesResenaApplication;

        public CalificacionResenaController(ICalificacionResenaApplication calificacionesResenaApplication)
        {
            _calificacionesResenaApplication = calificacionesResenaApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListCalificacionesResenas([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _calificacionesResenaApplication.ListCalificacionesResenas(filters);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CalificacionResenaById(int id)
        {
            var response = await _calificacionesResenaApplication.CalificacionResenaById(id);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("Producto/{productoId:int}")]
        public async Task<IActionResult> CalificacionesResenasByProductoId(int productoId)
        {
            var response = await _calificacionesResenaApplication.CalificacionesResenasByProductoId(productoId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCalificacionResena([FromBody] CalificacionResenaRequestDto requestDto)
        {
            var response = await _calificacionesResenaApplication.RegisterCalificacionResena(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditCalificacionResena(int id, [FromBody] CalificacionResenaRequestDto requestDto)
        {
            var response = await _calificacionesResenaApplication.EditCalificacionResena(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveCalificacionResena(int id)
        {
            var response = await _calificacionesResenaApplication.RemoveCalificacionResena(id);

            return Ok(response);
        }
    }
}
