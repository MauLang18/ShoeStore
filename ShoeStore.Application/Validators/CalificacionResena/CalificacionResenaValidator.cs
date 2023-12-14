using FluentValidation;
using ShoeStore.Application.Dtos.CalificacionResena.Request;

namespace ShoeStore.Application.Validators.CalificacionResena
{
    public class CalificacionResenaValidator : AbstractValidator<CalificacionResenaRequestDto>
    {
        public CalificacionResenaValidator()
        {
            RuleFor(x => x.Calificacion)
                .NotNull().WithMessage("El campo CALIFICACION no puede ser nulo")
                .NotEmpty().WithMessage("El campo CALIFICACION no puede estar vacio");

            RuleFor(x => x.Resena)
                .NotNull().WithMessage("El campo RESENA no puede ser nulo")
                .NotEmpty().WithMessage("El campo RESENA no puede estar vacio");
        }
    }
}
