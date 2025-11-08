using Login.Application.DTOs.Auth;
using Login.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Login.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _authService.LoginAsync(loginRequest);

        if (response == null)
        {
            // Damos un mensaje genérico por seguridad
            return Unauthorized("Email o contraseña incorrectos.");
        }

        return Ok(response); // Devuelve el DTO con el Token
    }
}