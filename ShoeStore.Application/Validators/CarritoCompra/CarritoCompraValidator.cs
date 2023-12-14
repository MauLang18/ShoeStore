using FluentValidation;
using ShoeStore.Application.Dtos.CarritoCompra.Request;

namespace ShoeStore.Application.Validators.CarritoCompra
{
    public class CarritoCompraValidator : AbstractValidator<CarritoCompraRequestDto>
    {
        public CarritoCompraValidator()
        {
            RuleFor(x => x.Cantidad)
                .NotNull().WithMessage("El campo CANTIDAD no puede ser nulo")
                .NotEmpty().WithMessage("El campo CANTIDAD no puede estar vacio");
        }
    }
}
