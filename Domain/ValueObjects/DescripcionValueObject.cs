using BibliotecaDigital.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Domain.ValueObjects
{
    public record DescripcionValueObject
    {
        public string Valor { get; }
        public DescripcionValueObject(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new DomainValidationException("La descripción es obligatoria y no puede estar vacía.");
            }
            if (valor.Length > 1000)
            {
                throw new DomainValidationException("La descripción excede el límite permitido de 1000 caracteres.");
            }
            Valor = valor.Trim();
        }
        
        //Entity Framework
        private DescripcionValueObject() 
        { 
            Valor = null!;
        }
    }
}
