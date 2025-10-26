using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Domain.Entities
{
    public class Rol
    {
        public Guid Id { get; set; }
        public required string nombre { get; set; }

        // Relaciones
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
