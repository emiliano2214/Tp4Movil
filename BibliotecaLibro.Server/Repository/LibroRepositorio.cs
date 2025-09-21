using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaLibro.Server.Repository
{
    public class LibroRepositorio
    {
        private readonly BibliotecaDbContext _context;

        public LibroRepositorio(BibliotecaDbContext context)
        {
            _context = context;
        }

        // Obtiene todos los libros
        public List<Libro> AccederInformacion()
        {
            return _context.Libro.ToList();
        }

        // Obtiene un libro por Id
        public Libro ObtenerPorId(int id)
        {
            return _context.Libro.FirstOrDefault(l => l.IdLibro == id);
        }

        // Agrega un libro nuevo
        public void Agregar(Libro libro)
        {
            _context.Libro.Add(libro);
            _context.SaveChanges();
        }

        // Actualiza un libro existente
        public void Actualizar(Libro libro)
        {
            _context.Libro.Update(libro);
            _context.SaveChanges();
        }

        // Elimina un libro
        public void Eliminar(Libro libro)
        {
            _context.Libro.Remove(libro);
            _context.SaveChanges();
        }
    }
}

