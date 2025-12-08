using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Domain.ValueObjects;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;

namespace BibliotecaDigital.Application.Services;

public class AutorService(IAutorRepository autorRepository)
{
    public readonly IAutorRepository _autorRepository = autorRepository;

    public async Task<Autor?> Handle(CrearAutorCommand command)
    {
        var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
        var apellidoVO = new NombreApellidoTituloValueObject(command.Apellido);
        
        var nuevoAutor = new Autor(nombreVO, apellidoVO, command.FechaNacimiento, new List<Libro>());

        var autorCreado = await _autorRepository.CrearAutorAsync(nuevoAutor);

        return autorCreado;
    }

    public async Task<Autor?> Handle(ActualizarAutorCommand command)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(command.Id);
        
        if (autor != null)
        {

            var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
            var apellidoVO = new NombreApellidoTituloValueObject(command.Apellido);
            
            autor.ActualizarAutor(nombreVO, apellidoVO, command.FechaNacimiento);

            var autorActualizado = await _autorRepository.ActualizarAutorAsync(autor);

            return autorActualizado;
        }
        
        return null;
    }
    
    public async Task<Autor?> Handle(EliminarAutorCommand command)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(command.Id);
        
        if (autor != null)
        {
            await _autorRepository.EliminarAutorAsync(autor.Id);
            return autor;
        }
        
        return null;
    }

    public async Task<Autor?> Handle(BuscarAutorQuery query)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(query.Id);
        
        return autor;
    }

    public async Task<List<Autor>> Handle(ListarAutoresQuery query)
    {
        var autores = await _autorRepository.ListarAutoresAsync(query.PageNumber, query.PageSize);
        
        return autores.ToList();
    }
}
