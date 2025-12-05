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

            builder.Entity<Libro>().OwnsOne(l => l.Titulo);
            builder.Entity<Libro>().OwnsOne(l => l.Descripcion);
            
            // 2. Configuración para la Entidad Autor
            builder.Entity<Autor>().OwnsOne(a => a.Nombre);
            builder.Entity<Autor>().OwnsOne(a => a.Apellido);

            // 3. Configuración para la Entidad Plan
            builder.Entity<Plan>().OwnsOne(p => p.Nombre);
            builder.Entity<Plan>().OwnsOne(p => p.Descripcion);

            // Configuraciones personalizadas van aquí (ej. DeleteBehavior)
        }
    }
}


