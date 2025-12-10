using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface ISubscripcionRepository
{
    Task<Subscripcion?> CrearSubscripcionAsync(Subscripcion subscripcion);
    Task<Subscripcion?> ObtenerSubscripcionPorIdAsync(Guid id);
    Task<IEnumerable<Subscripcion>> ListarSubscripcionesAsync(int pageNumber, int pageSize);
    Task<Subscripcion?> ActualizarSubscripcionAsync(Subscripcion subscripcion);

}
