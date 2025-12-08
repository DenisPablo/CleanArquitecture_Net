using AutoMapper;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Autor, AutorResponse>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Valor))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido.Valor))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
        ;
    }
}
