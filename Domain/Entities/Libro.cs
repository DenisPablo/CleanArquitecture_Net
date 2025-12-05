using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.ValueObjects;
using System.Linq;

namespace BibliotecaDigital.Domain.Entities
{
    public class Libro
    {
        public int Id { get; private set; } 
        public NombreApellidoTituloValueObject Titulo { get; private set; }
        public DescripcionValueObject Descripcion { get; private set; }
        public DateTime FechaPublicacion { get; private set; }
        public ICollection<Autor> Autores { get; private set; }
        public Estados Estado { get; private set; }
        
        public Libro(NombreApellidoTituloValueObject titulo, DescripcionValueObject descripcion, DateTime fechaPublicacion, ICollection<Autor> autores)
        {
            var errors = new List<string>();

            if (titulo == null)
            {
                errors.Add("El título del libro no puede ser nulo.");
            }
            if (descripcion == null)
            {
                errors.Add("La descripción del libro no puede ser nula.");
            }

            if (fechaPublicacion.Date > DateTime.Now.Date)
            {
                errors.Add("La fecha de publicación no puede ser futura.");
            }
            if (autores == null || !autores.Any())
            {
                errors.Add("El libro debe tener al menos un autor asociado.");
            }

            if (errors.Any())
            {
                throw new DomainValidationException(errors);
            }

            Titulo = titulo!;
            Descripcion = descripcion!;
            FechaPublicacion = fechaPublicacion;
            Autores = new List<Autor>(autores);
        }

        private Libro() { }

        public void Desactivar()
        {
            this.Estado = Estados.Inactivo;
        }

        public void Activar()
        {
            this.Estado = Estados.Activo;
        }
    }
}