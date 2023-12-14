using AutoMapper;
using ShoeStore.Application.Dtos.Pedido.Request;
using ShoeStore.Application.Dtos.Pedido.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class PedidoMappingProfile : Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<TbPedido, PedidoResponseDto>()
                .ForMember(x => x.NombreComprador, x => x.MapFrom(y => y.CompradorNavigation.Nombre))
                .ForMember(x => x.ApellidoComprador, x => x.MapFrom(y => y.CompradorNavigation.PrimerApellido))
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ForMember(x => x.Precio, x => x.MapFrom(y => y.ProductoNavigation.Precio))
                .ForMember(x => x.DireccionEnvio, x => x.MapFrom(y => y.DireccionEnvioNavigation.Direccion))
                .ForMember(x => x.MetodoPago, x => x.MapFrom(y => y.MetodoPagoNavigation.Metodo))
                .ForMember(x => x.Estatus, x => x.MapFrom(y => Enum.GetName(typeof(StatePedidoTypes), y.EstatusPedido)))
                .ForMember(x => x.EstadoPedido, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbPedido>, BaseEntityResponse<PedidoResponseDto>>().
                ReverseMap();

            CreateMap<PedidoRequestDto, TbPedido>();

            CreateMap<TbPedido, PedidoSelectResponseDto>()
                .ForMember(x => x.Producto, x => x.MapFrom(y => y.ProductoNavigation.Producto))
                .ForMember(x => x.Precio, x => x.MapFrom(y => y.ProductoNavigation.Precio))
                .ForMember(x => x.DireccionEnvio, x => x.MapFrom(y => y.DireccionEnvioNavigation.Direccion))
                .ForMember(x => x.MetodoPago, x => x.MapFrom(y => y.MetodoPagoNavigation.Metodo))
                .ForMember(x => x.Estatus, x => x.MapFrom(y => y.EstatusPedido.Equals(Enum.GetName(typeof(StatePedidoTypes), y.EstatusPedido))))
                .ReverseMap();
        }
    }
}
