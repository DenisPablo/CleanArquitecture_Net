using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Persistence;

public class SubscripcionRepository(ApplicationDbContext context) : ISubscripcionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Subscripcion?> CrearSubscripcionAsync(Subscripcion subscripcion)
    {
        try
        {
            _context.Subscripciones.Add(subscripcion);
            await _context.SaveChangesAsync();
            return subscripcion;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al crear subscripción", ex);
        }
    }

    public async Task<Subscripcion?> ObtenerSubscripcionPorIdAsync(Guid id)
    {
        try
        {
            return await _context.Subscripciones.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener subscripción por ID", ex);
        }
    }

    public async Task<IEnumerable<Subscripcion>> ListarSubscripcionesAsync(int pageNumber, int pageSize)
    {
        try
        {
            return await _context.Subscripciones
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener las subscripciones", ex);
        }
    }

    public async Task<Subscripcion?> ActualizarSubscripcionAsync(Subscripcion subscripcion)
    {
        try
        {
            _context.Subscripciones.Update(subscripcion);
            await _context.SaveChangesAsync();
            return subscripcion;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al actualizar subscripción", ex);
        }
    }


}
