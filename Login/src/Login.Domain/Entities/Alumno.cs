using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Domain.Entities
{
    public class Alumno
    {
        public Guid Id { get; set; }
        public decimal altura_cm { get; set; }
        public decimal peso_kg { get; set; }
        public DateTimeOffset fecha_nacimiento { get; set; }
        public string notas { get; set; }
        public required string Direccion { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid? entrenador_id { get; set; }
        public Usuario? Entrenador { get; set; }
        // Relaciones
        public required Usuario Usuario { get; set; }
    }
}
