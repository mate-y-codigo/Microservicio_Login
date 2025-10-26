using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Login.Infrastructure.Persistence;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        return usuario;
    }

    // --- LÓGICA IMPLEMENTADA ---

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        
        return await _context.Usuarios
            .Where(u => u.activo == true)
            .AsNoTracking()
            .ToListAsync();
    }
}