using BibliotecaDigital.Domain.Exceptions;
using System;

namespace BibliotecaDigital.Domain.Entities
{
    public class Subscripcion
    {
        public Guid Id { get; private set; }
        public Guid PlanId { get; private set; }
        public Plan Plan { get; private set; }
        
        public Guid ApplicationUserId { get; private set; } 

        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public Estados Estado { get; private set; }
        
        public Subscripcion(Guid planId, Guid applicationUserId,Plan plan, DateTime fechaInicio, DateTime fechaFin)
        {
            var errores = new List<string>();

            if (fechaFin <= fechaInicio)
            {
                errores.Add("La fecha de fin debe ser posterior a la fecha de inicio.");
            }

            if (plan is null)
            {
                errores.Add("El plan no puede ser nulo.");
            }

            if (errores.Any())
            {
                throw new DomainValidationException(errores);
            }

            this.Plan = plan!;
            this.PlanId = planId;
            this.ApplicationUserId = applicationUserId;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
        }

        //Entity Framework
        private Subscripcion() 
        { 
            Plan = null!;
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