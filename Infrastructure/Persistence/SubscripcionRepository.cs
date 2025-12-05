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

    public async Task<Subscripcion?> ObtenerSubscripcionPorIdAsync(int id)
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

    public async Task<IEnumerable<Subscripcion>> ObtenerTodosLasSubscripcionesAsync()
    {
        try
        {
            return await _context.Subscripciones.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener todas las subscripciones", ex);
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

    public async Task EliminarSubscripcionAsync(int id)
    {
        try
        {
            var subscripcion = await ObtenerSubscripcionPorIdAsync(id);
            if (subscripcion != null)
            {
                subscripcion.Desactivar();
                _context.Update(subscripcion);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al eliminar subscripción", ex);
        }
    }
}
