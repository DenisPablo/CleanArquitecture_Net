using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface IAutorRepository
{
   Task<Autor?>CrearAutorAsync(Autor autor);
   Task<Autor?>ObtenerAutorPorIdAsync(Guid id);
   Task<IEnumerable<Autor>>ObtenerTodosLosAutoresAsync();

   Task<Autor?>ActualizarAutorAsync(Autor autor);
   Task<Autor?> EliminarAutorAsync(Guid id);   
}