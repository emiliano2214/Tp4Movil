using System;

namespace BibliotecaLibro.Server.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string NombreLibro { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaEmision { get; set; }
        public string Imagen { get; set; }

        // Constructor sin parámetros requerido por EF Core
        public Libro() { }

        // Constructor con parámetros para uso manual
        public Libro(int id, string nombre, string descripcion, DateOnly fecha, string imagen)
        {
            IdLibro = id;
            NombreLibro = nombre;
            Descripcion = descripcion;
            FechaEmision = fecha;
            Imagen = imagen;
        }
    }
}
