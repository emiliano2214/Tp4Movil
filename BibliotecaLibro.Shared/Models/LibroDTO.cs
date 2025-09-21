using System;

namespace BibliotecaLibro.Shared.Models
{
    public class LibroDTO
    {
        public int IdLibro { get; set; }
        public string NombreLibro { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaEmision { get; set; }
        public string Imagen { get; set; }

        // Constructor sin parámetros requerido por EF Core
        public LibroDTO() { }

        // Constructor con parámetros para uso manual
        public LibroDTO(int id, string nombre, string descripcion, DateOnly fecha, string imagen)
        {
            IdLibro = id;
            NombreLibro = nombre;
            Descripcion = descripcion;
            FechaEmision = fecha;
            Imagen = imagen;
        }
    }
}
