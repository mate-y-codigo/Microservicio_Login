using Login.Application.DTOs.Usuario;
using Login.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Login.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST /api/Usuarios
        [HttpPost]
        [Authorize(Roles = "Admin, Entrenador")] // <-- Solo Admins y Entrenadores
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreateDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var callerRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(callerRole))
            {
                return Unauthorized("El token no contiene un rol válido.");
            }

            try
            {
                var nuevoUsuario = await _usuarioService.CreateUsuarioAsync(usuarioDto, callerRole);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                // Devolvemos 400 (Bad Request) con el mensaje de error de la lógica de negocio
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET /api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // GET /api/Usuarios
        [HttpGet]
        [Authorize(Roles = "Admin, Entrenador")] // <-- Solo Admins y Entrenadores
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("actives")]
        [Authorize(Roles = "Admin, Entrenador")] // <-- Solo Admins y Entrenadores
        public async Task<IActionResult> GetAllAlumnosActivosAsync()
        {
            var usuarios = await _usuarioService.GetAllAlumnosActivosAsync();
            return Ok(usuarios);
        }

        // PUT /api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(Guid id, [FromBody] UsuarioUpdateDto updateDto)
        {
            var callerRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(callerRole))
                return Unauthorized("El token no contiene un rol válido.");

            var callerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(callerIdString))
                return Unauthorized("El token no contiene un ID de usuario (NameIdentifier).");

            if (!Guid.TryParse(callerIdString, out Guid callerId))
                return Unauthorized("El ID de usuario en el token no es un GUID válido.");

            var result = await _usuarioService.UpdateUsuarioAsync(id, updateDto, callerRole, callerId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE /api/Usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // <-- Solo Admins
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
