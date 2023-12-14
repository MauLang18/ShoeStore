using AutoMapper;
using ShoeStore.Application.Dtos.Usuario.Request;
using ShoeStore.Application.Dtos.Usuario.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<TbUsuario, UsuarioResponseDto>()
                .ForMember(x => x.Rol, x => x.MapFrom(y => y.RolNavigation.Rol))
                .ForMember(x => x.EstadoUsuario, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbUsuario>, BaseEntityResponse<UsuarioResponseDto>>()
                .ReverseMap();

            CreateMap<UsuarioRequestDto, TbUsuario>();

            CreateMap<TokenRequestDto, TbUsuario>();
        }
    }
}