using Login.Application.DTOs.Auth;

namespace Login.Application.Interfaces;

public interface IAuthService
{
    // Devuelve un DTO con el token si el login es exitoso, o null si falla
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
}