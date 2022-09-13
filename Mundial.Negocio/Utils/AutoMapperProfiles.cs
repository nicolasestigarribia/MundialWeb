using AutoMapper;
using Mundial.Entidades;
using Mundial.Entidades.LoginModel;
namespace Mundial.Negocio.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //CreateMap<UsuarioUpdateRequestDto, Usuario>().ReverseMap();
            CreateMap<UsuarioInsertRequestDto, Usuario>().ReverseMap();
            CreateMap<UsuarioInsertRequestDto, Persona>().ReverseMap();
            //CreateMap<Usuario, UsuarioResponseDto>().ReverseMap();
        }

    }
}
