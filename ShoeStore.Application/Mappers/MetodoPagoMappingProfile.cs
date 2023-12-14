using AutoMapper;
using ShoeStore.Application.Dtos.MetodoPago.Request;
using ShoeStore.Application.Dtos.MetodoPago.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class MetodoPagoMappingProfile : Profile
    {
        public MetodoPagoMappingProfile()
        {
            CreateMap<TbMetodoPago, MetodoPagoResponseDto>()
                .ForMember(x => x.EstadoMetodoPago, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<TbMetodoPago>, BaseEntityResponse<MetodoPagoResponseDto>>()
                .ReverseMap();

            CreateMap<MetodoPagoRequestDto, TbMetodoPago>();

            CreateMap<TbMetodoPago, MetodoPagoSelectResponseDto>()
                .ReverseMap();
        }
    }
}
