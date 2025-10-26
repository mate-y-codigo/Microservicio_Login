using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Application.DTOs.Usuario;

using Login.Application.DTOs.Usuario;

namespace Login.Application.Interfaces
{
    public interface IUsuarioService
    {
        // Método para crear un usuario (Alta)
        Task<UsuarioReadDto> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto);

        // Método para actualizar (Modificación)
        Task<bool> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto usuarioUpdateDto);

        // Método para eliminar (Baja)
        Task<bool> DeleteUsuarioAsync(Guid id);

        // Método de lectura por ID
        Task<UsuarioReadDto?> GetUsuarioByIdAsync(Guid id);

        // Método de lectura de todos
        Task<IEnumerable<UsuarioReadDto>> GetAllUsuariosAsync();
    }
}
