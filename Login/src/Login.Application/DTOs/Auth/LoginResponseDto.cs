namespace Login.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Nombre { get; set; }
}