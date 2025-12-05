using BibliotecaDigital.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Domain.ValueObjects
{
    public record NombreApellidoTituloValueObject
    {
        public string Valor { get; }

        public NombreApellidoTituloValueObject(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new DomainValidationException("El nombre o título es obligatorio y no puede estar vacío.");
            }
            if (valor.Length > 100)
            {
                throw new DomainValidationException("El nombre o título excede el límite permitido de 100 caracteres.");
            }
            Valor = valor.Trim();
        }

        //Entity Framework
        private NombreApellidoTituloValueObject() { }
    }
}
