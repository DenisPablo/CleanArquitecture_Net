using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Persistence
{
    public class LibroRepository(ApplicationDbContext context) : ILibroRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Libro?> CrearLibroAsync(Libro libro)
        {
            try
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return libro;
            }
            catch(Exception ex){
                throw new PersistenceExeption("Error al crear libro", ex);
            }
        }
       
       public async Task<Libro?> ObtenerLibroPorIdAsync(Guid id)
       {
           try
           {
               return await _context.Libros
                   .FirstOrDefaultAsync(l => l.Id == id);
           }
           catch (Exception ex)
           {
               throw new PersistenceExeption("Error al obtener el libro por ID", ex);
           }
       }

       public async Task<IEnumerable<Libro>> ListarLibrosAsync(int pageNumber, int pageSize)
       {
           try
           {
               return await _context.Libros
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();
           }
           catch (Exception ex)
           {
               throw new PersistenceExeption("Error al obtener los libros", ex);
           }
       }

       public async Task<Libro?> ActualizarLibroAsync(Libro libro)
       {
           try
           {
               _context.Update(libro);
               await _context.SaveChangesAsync();
               return libro;
           }
           catch (Exception ex)
           {
               throw new PersistenceExeption("Error al actualizar el libro", ex);
           }
       }

       public async Task EliminarLibroAsync(Guid id)
       {
           try
           {
               var libro = await ObtenerLibroPorIdAsync(id);
               if (libro != null)
               {
                    libro.Desactivar();
                   _context.Update(libro);
                   await _context.SaveChangesAsync();
               }
           }
           catch (Exception ex)
           {
               throw new PersistenceExeption("Error al eliminar el libro", ex);
           }
       }
    }
}