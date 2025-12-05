using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface IAutorRepository
{
   Task<Autor?>CrearAutorAsync(Autor autor);
   Task<Autor?>ObtenerAutorPorIdAsync(int id);
   Task<IEnumerable<Autor>>ObtenerTodosLosAutoresAsync();

   Task<Autor?>ActualizarAutorAsync(Autor autor);
   Task<Autor?> EliminarAutorAsync(int id);   
}