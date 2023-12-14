using AutoMapper;
using ShoeStore.Application.Dtos.Direccion.Request;
using ShoeStore.Application.Dtos.Direccion.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class DireccionMappingProfile : Profile
    {
        public DireccionMappingProfile()
        {
            CreateMap<TbDireccion, DireccionResponseDto>()
                .ForMember(x => x.NombreComprador, x => x.MapFrom(y => y.CompradorNavigation.Nombre))
                .ForMember(x => x.ApellidoComprador, x => x.MapFrom(y => y.CompradorNavigation.PrimerApellido))
                .ForMember(x => x.EstadoDireccion, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbDireccion>, BaseEntityResponse<DireccionResponseDto>>().
                ReverseMap();

            CreateMap<DireccionRequestDto, TbDireccion>();

            CreateMap<TbDireccion, DireccionSelectResponseDto>()
                .ReverseMap();
        }
    }
}
