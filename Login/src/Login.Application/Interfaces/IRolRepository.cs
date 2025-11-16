using Login.Domain.Entities;

namespace Login.Application.Interfaces
{
    public interface IRolRepository
    {
        Task<Rol?> GetByIdAsync(int id);
    }
}