using FluentValidation;
using ShoeStore.Application.Dtos.Direccion.Request;

namespace ShoeStore.Application.Validators.Direccion
{
    public class DireccionValidator : AbstractValidator<DireccionRequestDto>
    {
        public DireccionValidator()
        {
            RuleFor(x => x.Direccion)
                .NotNull().WithMessage("El campo DIRECCION no puede ser nulo")
                .NotEmpty().WithMessage("El campo DIRECCION no puede estar vacio");
        }
    }
}
