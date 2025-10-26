using Login.Application.DTOs.Usuario;
using Login.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Login.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Ruta base: /api/Usuarios
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    // El servicio es inyectado automáticamente gracias a Program.cs
    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // --- ALTA (Crear Usuario) ---
    // POST /api/Usuarios
    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreateDto usuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var nuevoUsuario = await _usuarioService.CreateUsuarioAsync(usuarioDto);
            // Devuelve un 201 Created con la ubicación del nuevo recurso y el DTO de lectura
            return CreatedAtAction(nameof(GetUsuarioById), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }
        catch (Exception ex)
        {
            // Manejo de errores (ej. email duplicado)
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    // --- LECTURA (Obtener Usuario) ---
    // GET /api/Usuarios/123e4567-e89b-12d3-a456-426614174000
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

    // --- MODIFICACIÓN (Actualizar Usuario) ---
    // PUT /api/Usuarios/123e4567-e89b-12d3-a456-426614174000
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(Guid id, [FromBody] UsuarioUpdateDto updateDto)
    {
        var result = await _usuarioService.UpdateUsuarioAsync(id, updateDto);
        if (!result)
        {
            return NotFound(); // O BadRequest si la actualización falló
        }
        return NoContent(); // 204 No Content es la respuesta estándar para un PUT exitoso
    }

    // --- BAJA (Eliminar Usuario) ---
    // DELETE /api/Usuarios/123e4567-e89b-12d3-a456-426614174000
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(Guid id)
    {
        var result = await _usuarioService.DeleteUsuarioAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent(); // 204 No Content
    }
}
