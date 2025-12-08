using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Infrastructure.Identity;
using BibliotecaDigital.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ISubscripcionRepository, SubscripcionRepository>();

builder.Services.AddScoped<AutorService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
