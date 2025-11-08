using Login.Application.DTOs.Usuario;
using Login.Application.Interfaces;
using Login.Domain.Entities;
using BCrypt.Net;
using System.Linq;

namespace Login.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }

        public async Task<UsuarioReadDto> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto, string callerRole)
        {
            // 1. Verificar si el email ya existe
            if (await _usuarioRepository.GetByEmailAsync(usuarioCreateDto.Email) != null)
            {
                throw new Exception("El email ya está en uso.");
            }

            // 2. Obtener el nombre del rol que se quiere asignar
            var rolParaAsignar = await _rolRepository.GetByIdAsync(usuarioCreateDto.RolId);
            if (rolParaAsignar == null)
            {
                throw new Exception("El RolId proporcionado no existe.");
            }

            // 3. --- LÓGICA DEL PROFESOR ---
            if (callerRole == "Admin" && rolParaAsignar.nombre != "Entrenador")
            {
                throw new Exception("Un Administrador solo puede crear usuarios con el rol 'Entrenador'.");
            }

            if (callerRole == "Entrenador" && rolParaAsignar.nombre != "Alumno")
            {
                throw new Exception("Un Entrenador solo puede crear usuarios con el rol 'Alumno'.");
            }
            // --- FIN LÓGICA ---

            // 4. Si toda la lógica pasa, crear el usuario
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuarioCreateDto.Password);

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
                activo = true, // Asegurarse de que esté activo
                AlumnosEntrenados = new List<Alumno>()
            };

            await _usuarioRepository.AddAsync(nuevoUsuario);

            // 5. Mapear y devolver
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

        public async Task<bool> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id); // (Ahora solo encuentra usuarios activos)
            if (usuario == null)
            {
                return false;
            }

            usuario.nombre = usuarioUpdateDto.Nombre;
            usuario.apellido = usuarioUpdateDto.Apellido;
            usuario.celular = usuarioUpdateDto.Celular;
            usuario.actualizado_en = DateTimeOffset.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
            return true;
        }

        public async Task<bool> DeleteUsuarioAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id); // (Ahora solo encuentra usuarios activos)
            if (usuario == null)
            {
                return false;
            }

            // Lógica de Soft Delete
            usuario.activo = false;
            usuario.actualizado_en = DateTimeOffset.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
            return true;
        }

        public async Task<UsuarioReadDto?> GetUsuarioByIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id); // (Ahora solo encuentra usuarios activos)
            if (usuario == null)
            {
                return null;
            }

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
            var usuarios = await _usuarioRepository.GetAllAsync(); // (Ya filtra por 'activo == true')
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
}