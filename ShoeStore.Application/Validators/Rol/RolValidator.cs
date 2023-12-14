using FluentValidation;
using ShoeStore.Application.Dtos.Rol.Request;

namespace ShoeStore.Application.Validators.Rol
{
    public class RolValidator : AbstractValidator<RolRequestDto>
    {
        public RolValidator()
        {
            RuleFor(x => x.Rol)
                .NotNull().WithMessage("El campo ROL no puede ser nulo")
                .NotEmpty().WithMessage("El campo ROL no puede estar vacio");
        }
    }
}
