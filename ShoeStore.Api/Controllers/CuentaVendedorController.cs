using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.CuentaVendedor.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaVendedorController : ControllerBase
    {
        private readonly ICuentaVendedorApplication _cuentaVendedorApplication;

        public CuentaVendedorController(ICuentaVendedorApplication cuentaVendedorApplication)
        {
            _cuentaVendedorApplication = cuentaVendedorApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListCuentasVendedores([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _cuentaVendedorApplication.ListCuentasVendedores(filters);

            return Ok(response);
        }

        [HttpGet("Usuario/{userId:int}")]
        public async Task<IActionResult> CuentaVendedorByUser(int userId)
        {
            var response = await _cuentaVendedorApplication.CuentaVendedorByUser(userId);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CuentaVendedorById(int id)
        {
            var response = await _cuentaVendedorApplication.CuentaVendedorById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCuentaVendedor([FromBody] CuentaVendedorRequestDto requestDto)
        {
            var response = await _cuentaVendedorApplication.RegisterCuentVendedor(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditCuentaVendedor(int id, [FromBody] CuentaVendedorRequestDto requestDto)
        {
            var response = await _cuentaVendedorApplication.EditCuentaVendedor(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveCuentaVendedor(int id)
        {
            var response = await _cuentaVendedorApplication.RemoveCuentaVendedor(id);

            return Ok(response);
        }
    }
}
