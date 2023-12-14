using AutoMapper;
using ShoeStore.Application.Dtos.Producto.Request;
using ShoeStore.Application.Dtos.Producto.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class ProductoMappingProfile : Profile
    {
        public ProductoMappingProfile()
        {
            CreateMap<TbProducto, ProductoResponseDto>()
                .ForMember(x => x.Categoria, x => x.MapFrom(y => y.CategoriaNavigation.Categoria))
                .ForMember(x => x.OpcionEnvio, x => x.MapFrom(y => y.OpcionEnvioNavigation.Opcion))
                .ForMember(x => x.NombreVendedor, x => x.MapFrom(y => y.VendedorNavigation.Nombre))
                .ForMember(x => x.ApellidoVendedor, x => x.MapFrom(y => y.VendedorNavigation.PrimerApellido))
                .ForMember(x => x.Disponibilidad, x => x.MapFrom(y => y.Disponibilidad.Equals((int)DisponibilidadTypes.Disponible) ? "Disponible" : "Agotado"))
                .ForMember(x => x.EstadoProducto, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbProducto>, BaseEntityResponse<ProductoResponseDto>>().
                ReverseMap();

            CreateMap<ProductoRequestDto, TbProducto>();
        }
    }
}
