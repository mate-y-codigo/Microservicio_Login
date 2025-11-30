using Login.Application.DTOs.Usuario;

namespace Login.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioReadDto> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto, string callerRole);
        Task<bool> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto usuarioUpdateDto, string callerRole, Guid callerId);
        Task<bool> DeleteUsuarioAsync(Guid id);
        Task<UsuarioReadDto?> GetUsuarioByIdAsync(Guid id);
        Task<IEnumerable<UsuarioReadDto>> GetAllUsuariosAsync();
        Task<IEnumerable<UsuarioReadDto>> GetAllAlumnosActivosAsync();
    }
}
