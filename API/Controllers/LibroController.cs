using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones relacionadas con libros
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LibroController(LibroService libroService) : ControllerBase
{
    private readonly LibroService _libroService = libroService;

    /// <summary>
    /// Crea un nuevo libro en el sistema
    /// </summary>
    [HttpPost("CrearLibro", Name = "CrearLibro")]
    [ProducesResponseType(typeof(Libro), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CrearLibro(CrearLibroCommand command)
    {
        try
        {
            var libro = await _libroService.Handle(command);

            return CreatedAtAction(nameof(BuscarLibro), new { id = libro!.Id }, libro);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al crear libro: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al crear libro: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Actualiza los datos de un libro existente
    /// </summary>
    [HttpPut("ActualizarLibro/{id:guid}", Name = "ActualizarLibro")]
    [ProducesResponseType(typeof(LibroResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ActualizarLibro(Guid id, ActualizarLibroCommand command)
    {
        try
        {
            // Ensure ID from URL matches command if needed, or override.
            // The command record usually has the ID.
            var commandToHandle = command with { Id = id };

            var libro = await _libroService.Handle(commandToHandle);

            if (libro == null) return NotFound();

            return Ok(libro);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al actualizar libro: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al actualizar libro: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error al actualizar libro: " + ex.Message);
        }
    }

    /// <summary>
    /// Desactiva un libro (eliminación lógica)
    /// </summary>
    [HttpDelete("EliminarLibro/{id:guid}", Name = "EliminarLibro")]
    [ProducesResponseType(typeof(LibroResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarLibro(Guid id)
    {
        try
        {
            var commandToHandle = new EliminarLibroCommand(id);

            var libro = await _libroService.Handle(commandToHandle);

            if (libro == null) return NotFound();

            return Ok(libro);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Busca un libro específico por su ID
    /// </summary>
    [HttpGet("BuscarLibro/{id:guid}", Name = "BuscarLibro")]
    [ProducesResponseType(typeof(LibroResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BuscarLibro(Guid id)
    {
        try
        {
            var commandToHandle = new BuscarLibroQuery(id);

            var libro = await _libroService.Handle(commandToHandle);

            if (libro == null) return NotFound();

            return Ok(libro);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al buscar libro: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al buscar libro: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Obtiene una lista paginada de libros
    /// </summary>
    [HttpGet("ListarLibros", Name = "ListarLibros")]
    [ProducesResponseType(typeof(List<LibroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ListarLibros([FromQuery] ListarLibrosQuery query)
    {
        try
        {
            var libros = await _libroService.Handle(query);
            return Ok(libros);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
