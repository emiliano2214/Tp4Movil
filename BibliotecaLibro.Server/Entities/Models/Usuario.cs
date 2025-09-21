using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaLibro.Server.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Privilegio { get; set; }
        public string Img {get; set;}

        public Usuario() { }
        public Usuario(int id, string nombre, string contraseña, string privilegio, string img)
        {
            IdUsuario = id;
            NombreUsuario = nombre;
            Contraseña = contraseña;
            Privilegio = privilegio;
            Img = img;
        }
    }
}
