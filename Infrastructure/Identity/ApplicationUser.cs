using Microsoft.AspNetCore.Identity;
using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    //Campos base de IdentityUser
    public ICollection<Subscripcion> Subscripciones { get; set; } = new List<Subscripcion>();
}
