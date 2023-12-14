using AutoMapper;
using ShoeStore.Application.Dtos.OpcionEnvio.Request;
using ShoeStore.Application.Dtos.OpcionEnvio.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class OpcionEnvioMappingProfile : Profile
    {
        public OpcionEnvioMappingProfile()
        {
            CreateMap<TbOpcionEnvio, OpcionEnvioResponseDto>()
                .ForMember(x => x.EstadoOpcionEnvio, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<TbOpcionEnvio>, BaseEntityResponse<OpcionEnvioResponseDto>>()
                .ReverseMap();

            CreateMap<OpcionEnvioRequestDto, TbOpcionEnvio>();

            CreateMap<TbOpcionEnvio, OpcionEnvioSelectResponseDto>()
                .ReverseMap();
        }
    }
}
