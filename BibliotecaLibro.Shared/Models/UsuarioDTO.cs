using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaLibro.Shared.Models
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Privilegio { get; set; }
        public string Img {get; set;}

        public UsuarioDTO() { }
        public UsuarioDTO(int id, string nombre, string contraseña, string privilegio, string img)
        {
            IdUsuario = id;
            NombreUsuario = nombre;
            Contraseña = contraseña;
            Privilegio = privilegio;
            Img = img;
        }
    }
}
