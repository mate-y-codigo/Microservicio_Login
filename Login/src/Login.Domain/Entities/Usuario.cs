using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Login.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
      
        public string celular { get; set; }
        public Guid rol_id { get; set; }
        public DateTimeOffset creado_en { get; set; }
        public DateTimeOffset actualizado_en { get; set; }
        public bool activo { get; set; } = true;

        // Relaciones
        public Rol Rol { get; set; }
        public Alumno? Alumno { get; set; }
        public ICollection<Alumno> AlumnosEntrenados { get; set; }
    }
}
