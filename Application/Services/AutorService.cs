using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Domain.ValueObjects;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Application.Exeptions;

namespace BibliotecaDigital.Application.Services;

public class AutorService(IAutorRepository autorRepository)
{
    public readonly IAutorRepository _autorRepository = autorRepository;

    public async Task<Autor?> Handle(CrearAutorCommand command)
    {
        try
        {
            var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
            var apellidoVO = new NombreApellidoTituloValueObject(command.Apellido);
            
            var nuevoAutor = new Autor(nombreVO, apellidoVO, command.FechaNacimiento, new List<Libro>());

            var autorCreado = await _autorRepository.CrearAutorAsync(nuevoAutor);

        return autorCreado;
        }
        catch (DomainValidationException)
        {
            throw;
        }
        catch (PersistenceExeption)
        {
            throw;
        }
    }
}