using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaLibro.Server.Repository
{
    public class UsuarioRepositorio
    {
        private readonly BibliotecaDbContext _context;

        public UsuarioRepositorio(BibliotecaDbContext context)
        {
            _context = context;
        }

        // Obtiene todos los usuarios
        public List<Usuario> ObtenerUsuario()
        {
            return _context.Usuario
                
                .ToList();
        }

        // Obtiene un usuario por Id
        public Usuario ObtenerPorId(int id)
        {
            return _context.Usuario.FirstOrDefault(u => u.IdUsuario == id);
        }

        // Agrega un usuario nuevo
        public void Agregar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        // Actualiza un usuario existente
        public void Actualizar(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            _context.SaveChanges();
        }

        // Elimina un usuario
        public void Eliminar(Usuario usuario)
        {
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
