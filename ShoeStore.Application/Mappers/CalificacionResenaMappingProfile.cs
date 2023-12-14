using AutoMapper;
using ShoeStore.Application.Dtos.CalificacionResena.Request;
using ShoeStore.Application.Dtos.CalificacionResena.Response;
using ShoeStore.Application.Dtos.CarritoCompra.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class CalificacionResenaMappingProfile : Profile
    {
        public CalificacionResenaMappingProfile()
        {
            CreateMap<TbCalificacionResena, CalificacionResenaResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.UsuarioNavigation.Nombre))
                .ForMember(x => x.ApellidoUsuario, x => x.MapFrom(y => y.UsuarioNavigation.PrimerApellido))
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ForMember(x => x.EstadoCalificacionResena, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<TbCalificacionResena>, BaseEntityResponse<CalificacionResenaResponseDto>>()
                .ReverseMap();

            CreateMap<CalificacionResenaRequestDto, TbCalificacionResena>();

            CreateMap<TbCalificacionResena, CalificacionResenaSelectResponseDto>()
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.UsuarioNavigation.Nombre))
                .ForMember(x => x.ApellidoUsuario, x => x.MapFrom(y => y.UsuarioNavigation.PrimerApellido))
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ReverseMap();
        }
    }
}
