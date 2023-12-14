using FluentValidation;
using ShoeStore.Application.Dtos.OpcionEnvio.Request;

namespace ShoeStore.Application.Validators.OpcionEnvio
{
    public class OpcionEnvioValidator : AbstractValidator<OpcionEnvioRequestDto>
    {
        public OpcionEnvioValidator()
        {
            RuleFor(x => x.Opcion)
               .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
               .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");
        }
    }
}
