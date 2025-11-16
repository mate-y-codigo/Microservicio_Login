using Login.Application.Interfaces;
using Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.Infrastructure.Persistence
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _context;
        public RolRepository(AppDbContext context) { _context = context; }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
    }
}