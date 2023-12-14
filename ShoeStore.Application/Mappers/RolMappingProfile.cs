using AutoMapper;
using ShoeStore.Application.Dtos.Rol.Request;
using ShoeStore.Application.Dtos.Rol.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class RolMappingProfile : Profile
    {
        public RolMappingProfile()
        {
            CreateMap<TbRol, RolResponseDto>()
                .ForMember(x => x.EstadoRol, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbRol>, BaseEntityResponse<RolResponseDto>>()
                .ReverseMap();

            CreateMap<RolRequestDto, TbRol>();

            CreateMap<TbRol, RolSelectResponseDto>()
                .ReverseMap();
        }
    }
}
