using Login.Application.DTOs.Usuario;
using Login.Application.Interfaces;
using Login.Domain.Entities;
using BCrypt.Net;
using System.Linq; // <-- Asegúrate de tener este using

namespace Login.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioReadDto> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto)
    {
        // 1. Lógica de Application (Hashear)
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuarioCreateDto.Password);

        // 2. Lógica de Application (Mapear DTO a Entidad)
        var nuevoUsuario = new Usuario
        {
            Id = Guid.NewGuid(),
            email = usuarioCreateDto.Email,
            password = passwordHash,
            nombre = usuarioCreateDto.Nombre,
            apellido = usuarioCreateDto.Apellido,
            celular = usuarioCreateDto.Celular,
            rol_id = usuarioCreateDto.RolId,
            creado_en = DateTimeOffset.UtcNow,
            actualizado_en = DateTimeOffset.UtcNow,
            AlumnosEntrenados = new List<Alumno>() // Inicializar colección
        };

        // 3. Delegar el guardado (El "Cómo")
        await _usuarioRepository.AddAsync(nuevoUsuario);

        // 4. Lógica de Application (Mapear Entidad a DTO)
        return new UsuarioReadDto
        {
            Id = nuevoUsuario.Id,
            Email = nuevoUsuario.email,
            Nombre = nuevoUsuario.nombre,
            Apellido = nuevoUsuario.apellido,
            Celular = nuevoUsuario.celular,
            RolId = nuevoUsuario.rol_id,
            CreadoEn = nuevoUsuario.creado_en
        };
    }

    // --- LÓGICA IMPLEMENTADA ---

    public async Task<bool> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto usuarioUpdateDto)
    {
        // 1. Buscar el usuario
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        // 2. Comprobar si existe
        if (usuario == null)
        {
            return false; // Devuelve false si no se encuentra
        }

        // 3. Mapear los campos del DTO a la entidad
        // (No permitimos cambiar email, password o rol en este endpoint)
        usuario.nombre = usuarioUpdateDto.Nombre;
        usuario.apellido = usuarioUpdateDto.Apellido;
        usuario.celular = usuarioUpdateDto.Celular;
        usuario.actualizado_en = DateTimeOffset.UtcNow; // Actualizar timestamp

        // 4. Guardar los cambios en la BD
        await _usuarioRepository.UpdateAsync(usuario);

        return true; // Devuelve true si la actualización fue exitosa
    }

    public async Task<bool> DeleteUsuarioAsync(Guid id)
    {
        // 1. Buscar el usuario (GetByIdAsync ahora solo encuentra activos, está bien)
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        // 2. Comprobar si existe
        if (usuario == null)
        {
            return false; // No se encontró (o ya estaba inactivo)
        }

        // 3. --- ESTA ES LA LÓGICA DEL SOFT DELETE ---
        // En lugar de borrar, marcamos como inactivo
        usuario.activo = false;
        usuario.actualizado_en = DateTimeOffset.UtcNow; // Actualizamos el timestamp

        // 4. Guardamos los cambios usando el método 'Update'
        await _usuarioRepository.UpdateAsync(usuario);

        return true; // Devuelve true si la desactivación fue exitosa
    }

    public async Task<UsuarioReadDto?> GetUsuarioByIdAsync(Guid id)
    {
        // 1. Llama al repositorio para obtener la entidad
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        // 2. Si no se encuentra, devuelve null
        if (usuario == null)
        {
            return null;
        }

        // 3. Mapea la entidad (de la BD) al DTO (lo que ve el usuario)
        return new UsuarioReadDto
        {
            Id = usuario.Id,
            Email = usuario.email,
            Nombre = usuario.nombre,
            Apellido = usuario.apellido,
            Celular = usuario.celular,
            RolId = usuario.rol_id,
            CreadoEn = usuario.creado_en
        };
    }

    public async Task<IEnumerable<UsuarioReadDto>> GetAllUsuariosAsync()
    {
        // 1. Obtener todas las entidades de la BD
        var usuarios = await _usuarioRepository.GetAllAsync();

        // 2. Mapear la lista de entidades a una lista de DTOs
        var usuariosDto = usuarios.Select(usuario => new UsuarioReadDto
        {
            Id = usuario.Id,
            Email = usuario.email,
            Nombre = usuario.nombre,
            Apellido = usuario.apellido,
            Celular = usuario.celular,
            RolId = usuario.rol_id,
            CreadoEn = usuario.creado_en
        });

        return usuariosDto;
    }
}