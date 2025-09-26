using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaLibro.Shared.Models
{
    public class UsuarioCreateDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Privilegio { get; set; } 
        public string? Img { get; set; }
    }
}