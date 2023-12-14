using AutoMapper;
using ShoeStore.Application.Dtos.Categoria.Request;
using ShoeStore.Application.Dtos.Categoria.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class CategoriaMappingProfile : Profile
    {
        public CategoriaMappingProfile()
        {
            CreateMap<TbCategorium, CategoriaResponseDto>()
                .ForMember(x => x.EstadoCategoria, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbCategorium>, BaseEntityResponse<CategoriaResponseDto>>()
                .ReverseMap();

            CreateMap<CategoriaRequestDto, TbCategorium>();

            CreateMap<TbCategorium, CategoriaSelectResponseDto>()
                .ReverseMap();
        }
    }
}
