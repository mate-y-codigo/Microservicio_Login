using Login.Domain.Entities;

namespace Login.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario usuario);
        Task<Usuario?> GetByIdAsync(Guid id);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario); 
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<IEnumerable<Usuario>> GetAllAlumnosActivosAsync();
        Task<Usuario?> GetByEmailAsync(string email);
    }
}