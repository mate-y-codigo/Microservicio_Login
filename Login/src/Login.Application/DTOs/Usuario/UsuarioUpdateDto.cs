using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Application.DTOs.Usuario
{
    // ¡ASEGÚRATE DE QUE DICE 'public' AQUÍ!
    public class UsuarioUpdateDto
    {
        // Pon aquí las propiedades que se pueden actualizar
        // (Probablemente no incluyas Email, Password o RolId aquí)
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Celular { get; set; }
    }
}
