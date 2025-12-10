using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.ValueObjects;

namespace BibliotecaDigital.Domain.Entities
{
    public class Autor
    {
        public Guid Id { get; private set; }
        public NombreApellidoTituloValueObject Nombre { get; private set; }
        public NombreApellidoTituloValueObject Apellido { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public ICollection<Libro> Libros { get; private set; }
        public Estados Estado { get; private set; }

        public Autor(NombreApellidoTituloValueObject nombre, NombreApellidoTituloValueObject apellido, DateTime fechaNacimiento, ICollection<Libro> libros)
        {
            var errores = new List<string>();

            if (nombre == null)
            {
                errores.Add("El nombre del autor no puede ser nulo.");
            }

            if (apellido == null)
            {
                errores.Add("El apellido del autor no puede ser nulo.");
            }

            if (fechaNacimiento.Date > DateTime.Now.Date)
            {
                errores.Add("La fecha de nacimiento no puede ser futura.");
            }

            if (errores.Count != 0)
            {
                throw new DomainValidationException(errores);
            }

            this.Nombre = nombre!;
            this.Apellido = apellido!;
            this.FechaNacimiento = fechaNacimiento;
            this.Libros = new List<Libro>(libros!);
        }
        
        public void ActualizarAutor(NombreApellidoTituloValueObject nombre, NombreApellidoTituloValueObject apellido, DateTime fechaNacimiento)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.FechaNacimiento = fechaNacimiento;
        }

        //Entity Framework
        private Autor() 
        {
            Nombre = null!;
            Apellido = null!;
            Libros = new List<Libro>(); 
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