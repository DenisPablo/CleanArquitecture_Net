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
/// Controlador para gestionar operaciones relacionadas con subscripciones
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubscripcionController(SubscripcionService subscripcionService) : ControllerBase
{
    private readonly SubscripcionService _subscripcionService = subscripcionService;

    /// <summary>
    /// Crea una nueva subscripción en el sistema
    /// </summary>
    [HttpPost("CrearSubscripcion", Name = "CrearSubscripcion")]
    [ProducesResponseType(typeof(Subscripcion), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CrearSubscripcion(CrearSubscripcionCommand command)
    {
        try
        {
            var subscripcion = await _subscripcionService.Handle(command);

            return CreatedAtAction(nameof(BuscarSubscripcion), new { id = subscripcion!.Id }, subscripcion);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al crear subscripcion: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al crear subscripcion: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Actualiza los datos de una subscripción existente
    /// </summary>
    [HttpPut("ActualizarSubscripcion/{id:guid}", Name = "ActualizarSubscripcion")]
    [ProducesResponseType(typeof(SubscripcionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ActualizarSubscripcion(Guid id, ActualizarSubscripcionCommand command)
    {
        try
        {
            var commandToHandle = command with { Id = id };
            var subscripcion = await _subscripcionService.Handle(commandToHandle);

            if (subscripcion == null) return NotFound();

            return Ok(subscripcion);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al actualizar subscripcion: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al actualizar subscripcion: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error al actualizar subscripcion: " + ex.Message);
        }
    }

    /// <summary>
    /// Desactiva una subscripción (eliminación lógica)
    /// </summary>
    [HttpDelete("EliminarSubscripcion/{id:guid}", Name = "EliminarSubscripcion")]
    [ProducesResponseType(typeof(SubscripcionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarSubscripcion(Guid id)
    {
        try
        {
            var commandToHandle = new EliminarSubscripcionCommand(id);

            var subscripcion = await _subscripcionService.Handle(commandToHandle);

            if (subscripcion == null) return NotFound();

            return Ok(subscripcion);
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
    /// Busca una subscripción específica por su ID
    /// </summary>
    [HttpGet("BuscarSubscripcion/{id:guid}", Name = "BuscarSubscripcion")]
    [ProducesResponseType(typeof(SubscripcionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BuscarSubscripcion(Guid id)
    {
        try
        {
            var commandToHandle = new BuscarSubscripcionQuery(id);

            var subscripcion = await _subscripcionService.Handle(commandToHandle);

            if (subscripcion == null) return NotFound();

            return Ok(subscripcion);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al buscar subscripcion: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al buscar subscripcion: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Obtiene una lista paginada de subscripciones
    /// </summary>
    [HttpGet("ListarSubscripciones", Name = "ListarSubscripciones")]
    [ProducesResponseType(typeof(List<SubscripcionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ListarSubscripciones([FromQuery] ListarSubscripcionesQuery query)
    {
        try
        {
            var subscripciones = await _subscripcionService.Handle(query);
            return Ok(subscripciones);
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
