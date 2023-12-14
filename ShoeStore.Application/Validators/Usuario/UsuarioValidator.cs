using FluentValidation;
using ShoeStore.Application.Dtos.Usuario.Request;

namespace ShoeStore.Application.Validators.Usuario
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequestDto>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
                .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");
            RuleFor(x => x.Correo)
                .NotNull().WithMessage("El campo CORREO no puede ser nulo")
                .NotEmpty().WithMessage("El campo CORREO no puede estar vacio");
            RuleFor(x => x.Telefono)
                .NotNull().WithMessage("El campo TELEFONO no puede ser nulo")
                .NotEmpty().WithMessage("El campo TELEFONO no puede estar vacio");
            RuleFor(x => x.Contrasena)
                .NotNull().WithMessage("El campo CONTRASENA no puede ser nulo")
                .NotEmpty().WithMessage("El campo CONTRASENA no puede estar vacio");
        }
    }
}