using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.ValueObjects;
using System.Security.Cryptography.X509Certificates;

namespace BibliotecaDigital.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; private set; }
        public NombreApellidoTituloValueObject Nombre { get; private set; }
        public DescripcionValueObject Descripcion { get; private set; }
        public decimal Precio { get; set; }
        public ICollection<Subscripcion> Subscripciones { get; private set; }
        public Estados Estado { get; private set; }

        public Plan(NombreApellidoTituloValueObject nombre, DescripcionValueObject descripcion, decimal precio, ICollection<Subscripcion> subscripciones)
        {
            var errores = new List<string>();

            if (nombre == null)
            {
                errores.Add("El nombre del plan no puede ser nulo.");
            }

            if (descripcion == null)
            {
                errores.Add("La descripción del plan no puede ser nula.");
            }

            if (precio < 0)
            {
                errores.Add("El precio del plan no puede ser negativo.");
            }

            if (errores.Any())
            {
                throw new DomainValidationException(errores);
            }

            this.Nombre = nombre!;
            this.Descripcion = descripcion!;
            this.Precio = precio;
            this.Subscripciones = [.. subscripciones!];
        }
        //Entity Framework
        private Plan() 
        { 
            Nombre = null!;
            Descripcion = null!;
            Subscripciones = new List<Subscripcion>();
        }

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
