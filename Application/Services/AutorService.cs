using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Domain.ValueObjects;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using AutoMapper;
using BibliotecaDigital.Application.Response;

namespace BibliotecaDigital.Application.Services;

public class AutorService(IAutorRepository autorRepository, IMapper mapper)
{
    public readonly IAutorRepository _autorRepository = autorRepository;
    public readonly IMapper _mapper = mapper;

    public async Task<Autor?> Handle(CrearAutorCommand command)
    {
        var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
        var apellidoVO = new NombreApellidoTituloValueObject(command.Apellido);
        
        var nuevoAutor = new Autor(nombreVO, apellidoVO, command.FechaNacimiento, new List<Libro>());

        var autorCreado = await _autorRepository.CrearAutorAsync(nuevoAutor);

        return autorCreado;
    }

    public async Task<AutorResponse?> Handle(ActualizarAutorCommand command)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(command.Id);
        
        if (autor != null)
        {

            var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
            var apellidoVO = new NombreApellidoTituloValueObject(command.Apellido);
            
            autor.ActualizarAutor(nombreVO, apellidoVO, command.FechaNacimiento);

            var autorActualizado = await _autorRepository.ActualizarAutorAsync(autor);

            var autorResponse = _mapper.Map<AutorResponse>(autorActualizado);
            
            return autorResponse;
        }
        
            return null;
    }
    
    public async Task<AutorResponse?> Handle(EliminarAutorCommand command)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(command.Id);
        
        if (autor != null)
        {
            autor.Desactivar();
            autor = await _autorRepository.ActualizarAutorAsync(autor);
            
            var autorResponse = _mapper.Map<AutorResponse>(autor);            
            return autorResponse;
        }
        
        return null;
    }

    public async Task<AutorResponse?> Handle(BuscarAutorQuery query)
    {
        var autor = await _autorRepository.ObtenerAutorPorIdAsync(query.Id);
        
        return _mapper.Map<AutorResponse>(autor);
    }

    public async Task<List<AutorResponse>> Handle(ListarAutoresQuery query)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var autores = await _autorRepository.ListarAutoresAsync(pageNumber, pageSize);
        
        return _mapper.Map<List<AutorResponse>>(autores);
    }
}
