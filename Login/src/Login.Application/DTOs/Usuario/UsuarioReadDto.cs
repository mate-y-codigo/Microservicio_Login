using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Login.Application.DTOs.Usuario
{
    public class UsuarioReadDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Peso { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Altura { get; set; }
        public string? Celular { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public DateTimeOffset CreadoEn { get; set; }
    }
}
