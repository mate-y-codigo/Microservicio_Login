using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Login.Application.DTOs.Usuario
{
    public class UsuarioCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; } 

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        public string? Celular { get; set; }

        [Required]
        public Guid RolId { get; set; } // El ID del rol ("Alumno", "Entrenador", etc.)
    }
}
