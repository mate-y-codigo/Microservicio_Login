using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Application.DTOs.Usuario
{
    public class UsuarioUpdateDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Celular { get; set; }
    }
}
