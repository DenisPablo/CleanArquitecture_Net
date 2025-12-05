using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface ISubscripcionRepository
{
    Task<Subscripcion?> CrearSubscripcionAsync(Subscripcion subscripcion);
    Task<Subscripcion?> ObtenerSubscripcionPorIdAsync(int id);
    Task<IEnumerable<Subscripcion>> ObtenerTodosLasSubscripcionesAsync();
    Task<Subscripcion?> ActualizarSubscripcionAsync(Subscripcion subscripcion);
    Task EliminarSubscripcionAsync(int id);   
}
