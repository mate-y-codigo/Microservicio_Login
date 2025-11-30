using Login.Application.DTOs.Auth;
using Login.Application.Interfaces;
using Login.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        // 1. Buscar al usuario por email
        var usuario = await _usuarioRepository.GetByEmailAsync(loginRequest.Email);

        // 2. Validar que el usuario exista y esté activo
        if (usuario == null || !usuario.activo)
        {
            return null; // No autorizado (usuario no existe o está inactivo)
        }

        // 3. Validar la contraseña (usando BCrypt)
        bool esPasswordValido = BCrypt.Net.BCrypt.Verify(loginRequest.Password, usuario.password);
        if (!esPasswordValido)
        {
            return null; // No autorizado (contraseña incorrecta)
        }

        // 4. Si es válido, generar el token
        var token = GenerateJwtToken(usuario);

        return new LoginResponseDto
        {
            Token = token,
            Email = usuario.email,
            Nombre = usuario.nombre
        };
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("No se encontró la clave secreta de JWT (Jwt:Key).");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Crear los "claims" (información que va dentro del token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()), // ID del usuario
            new Claim(JwtRegisteredClaimNames.Email, usuario.email),
            new Claim("nombre", usuario.nombre + " " + usuario.apellido),
            new Claim(ClaimTypes.Role, usuario.Rol.nombre) // Añadimos el Rol
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8), // El token expira en 8 horas
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}