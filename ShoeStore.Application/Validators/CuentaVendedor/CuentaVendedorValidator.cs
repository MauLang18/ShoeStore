using FluentValidation;
using ShoeStore.Application.Dtos.CuentaVendedor.Request;

namespace ShoeStore.Application.Validators.CuentaVendedor
{
    public class CuentaVendedorValidator : AbstractValidator<CuentaVendedorRequestDto>
    {
        public CuentaVendedorValidator()
        {
            RuleFor(x => x.VentaTotal)
                .NotNull().WithMessage("El campo VENTA TOTAL no puede ser nulo")
                .NotEmpty().WithMessage("El campo VENTA TOTAL no puede estar vacio");

            RuleFor(x => x.CostoTotal)
                .NotNull().WithMessage("El campo COSTO TOTAL no puede ser nulo")
                .NotEmpty().WithMessage("El campo COSTO TOTAL no puede estar vacio");
        }
    }
}
