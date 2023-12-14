using AutoMapper;
using ShoeStore.Application.Dtos.CuentaVendedor.Request;
using ShoeStore.Application.Dtos.CuentaVendedor.Response;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Application.Mappers
{
    public class CuentaVendedorMappingProfile : Profile
    {
        public CuentaVendedorMappingProfile()
        {
            CreateMap<TbCuentaVendedor, CuentaVendedorResponseDto>()
                .ForMember(x => x.NombreVendedor, x => x.MapFrom(y => y.VendedorNavigation.Nombre))
                .ForMember(x => x.ApellidoVendedor, x => x.MapFrom(y => y.VendedorNavigation.PrimerApellido))
                .ForMember(x => x.Estatus, x => x.MapFrom(y => Enum.GetName(typeof(StateCuentaVendedorTypes), y.EstatusVendedor)))
                .ForMember(x => x.EstadoCuentaVendedor, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<TbCuentaVendedor>, BaseEntityResponse<CuentaVendedorResponseDto>>()
                .ReverseMap();

            CreateMap<CuentaVendedorRequestDto, TbCuentaVendedor>();
        }
    }
}
