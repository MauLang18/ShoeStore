using FluentValidation;
using ShoeStore.Application.Dtos.MetodoPago.Request;

namespace ShoeStore.Application.Validators.MetodoPago
{
    public class MetodoPagoValidator : AbstractValidator<MetodoPagoRequestDto>
    {
        public MetodoPagoValidator()
        {
            RuleFor(x => x.Metodo)
                .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
                .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");
        }
    }
}
