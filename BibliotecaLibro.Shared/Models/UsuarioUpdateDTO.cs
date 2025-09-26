using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaLibro.Shared.Models
{
    public class UsuarioUpdateDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Privilegio { get; set; } = "User";
        public string? Img { get; set; }
    }
}

