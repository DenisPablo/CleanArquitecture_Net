using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

    public record SubscripcionDTO
    {
        public Guid PlanId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Estados Estado { get; set; }
    }