using AutoMapper;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Domain.ValueObjects;

namespace BibliotecaDigital.Application.Services;

public class LibroService(ILibroRepository libroRepository, IAutorRepository autorRepository, IMapper mapper)
{
    public readonly ILibroRepository _libroRepository = libroRepository;
    public readonly IAutorRepository _autorRepository = autorRepository;
    public readonly IMapper _mapper = mapper;

    public async Task<Libro?> Handle(CrearLibroCommand command)
    {
        var tituloVO = new NombreApellidoTituloValueObject(command.Titulo);
        var descripcionVO = new DescripcionValueObject(command.Descripcion);

        var autores = new List<Autor>();
        foreach (var autorId in command.AutoresIds)
        {
            var autor = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            if (autor != null)
            {
                autores.Add(autor);
            }
        }

        if (!autores.Any())
        {
            // Optionally throw an exception if no valid authors are found, 
            // but domain validation in Libro constructor might catch empty list if enforced there.
            // Looking at Libro.cs, it throws if authors is empty.
        }

        var nuevoLibro = new Libro(tituloVO, descripcionVO, command.FechaPublicacion, autores);

        var libroCreado = await _libroRepository.CrearLibroAsync(nuevoLibro);

        return libroCreado;
    }

    public async Task<LibroResponse?> Handle(ActualizarLibroCommand command)
    {
        var libro = await _libroRepository.ObtenerLibroPorIdAsync(command.Id);

        if (libro != null)
        {
            var tituloVO = new NombreApellidoTituloValueObject(command.Titulo);
            var descripcionVO = new DescripcionValueObject(command.Descripcion);

            // Assuming we allow updating authors too. The command usually should have it.
            // Let's check ActualizarLibroCommand content.
            // If it has AutoresIds, we might need to fetch them.
            // If the command doesn't have AutoresIds, we keep existing or handle differently.
            // I'll check the command signature. Assuming it has AutoresIds for now based on CrearLibro pattern.
            var autores = new List<Autor>();
            foreach (var autorId in command.AutoresIds)
            {
                var autor = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
                if (autor != null)
                {
                    autores.Add(autor);
                }
            }

            libro.ActualizarLibro(tituloVO, descripcionVO, command.FechaPublicacion, autores);

            var libroActualizado = await _libroRepository.ActualizarLibroAsync(libro);

            var libroResponse = _mapper.Map<LibroResponse>(libroActualizado);

            return libroResponse;
        }

        return null;
    }

    public async Task<LibroResponse?> Handle(EliminarLibroCommand command)
    {
        var libro = await _libroRepository.ObtenerLibroPorIdAsync(command.Id);

        if (libro != null)
        {
            libro.Desactivar();
            libro = await _libroRepository.ActualizarLibroAsync(libro);

            var libroResponse = _mapper.Map<LibroResponse>(libro);
            return libroResponse;
        }

        return null;
    }

    public async Task<LibroResponse?> Handle(BuscarLibroQuery query)
    {
        var libro = await _libroRepository.ObtenerLibroPorIdAsync(query.Id);

        return _mapper.Map<LibroResponse>(libro);
    }

    public async Task<List<LibroResponse>> Handle(ListarLibrosQuery query)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var libros = await _libroRepository.ListarLibrosAsync(pageNumber, pageSize);

        return _mapper.Map<List<LibroResponse>>(libros);
    }
}
