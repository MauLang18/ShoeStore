using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.Rol.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolApplication _rolApplication;

        public RolController(IRolApplication rolApplication)
        {
            this._rolApplication = rolApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _rolApplication.ListRoles(filters);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectRoles()
        {
            var response = await _rolApplication.ListSelectRoles();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> RolById(int id)
        {
            var response = await _rolApplication.RolById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterRol([FromBody] RolRequestDto request)
        {
            var response = await _rolApplication.RegisterRol(request);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditRol(int id, [FromBody] RolRequestDto request)
        {
            var response = await _rolApplication.EditRol(id, request);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveRol(int id)
        {
            var response = await _rolApplication.RemoveRol(id);

            return Ok(response);
        }
    }
}
