using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Subscripcion> Subscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación muchos a muchos entre Libro y Autor
            builder.Entity<Libro>()
                .HasMany(l => l.Autores)
                .WithMany(a => a.Libros);

            // Configuraciones personalizadas van aquí (ej. DeleteBehavior)
        }
    }
}


