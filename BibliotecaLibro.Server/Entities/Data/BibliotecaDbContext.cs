using BibliotecaLibro.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaLibro.Server.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Libro> Libro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir claves primarias explícitamente
            modelBuilder.Entity<Libro>().HasKey(l => l.IdLibro);
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
        }
    }
}