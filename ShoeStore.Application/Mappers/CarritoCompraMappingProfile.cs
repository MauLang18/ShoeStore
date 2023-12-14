using AutoMapper;
using ShoeStore.Application.Dtos.CarritoCompra.Request;
using ShoeStore.Application.Dtos.CarritoCompra.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class CarritoCompraMappingProfile : Profile
    {
        public CarritoCompraMappingProfile()
        {
            CreateMap<TbCarritoCompra, CarritoCompraResponseDto>()
                .ForMember(x => x.NombreComprador, x => x.MapFrom(y => y.CompradorNavigation.Nombre))
                .ForMember(x => x.ApellidoComprador, x => x.MapFrom(y => y.CompradorNavigation.PrimerApellido))
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ForMember(x => x.Precio, x => x.MapFrom(y => y.ProductoNavigation.Precio))
                .ForMember(x => x.EstadoCarritoCompra, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbCarritoCompra>, BaseEntityResponse<CarritoCompraResponseDto>>().
                ReverseMap();

            CreateMap<CarritoCompraRequestDto, TbCarritoCompra>();

            CreateMap<TbCarritoCompra, CarritoCompraSelectResponseDto>()
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ForMember(x => x.Precio, x => x.MapFrom(y => y.ProductoNavigation.Precio))
                .ReverseMap();
        }
    }
}
