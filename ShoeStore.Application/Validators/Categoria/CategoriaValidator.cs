using FluentValidation;
using ShoeStore.Application.Dtos.Categoria.Request;

namespace ShoeStore.Application.Validators.Categoria
{
    public class CategoriaValidator : AbstractValidator<CategoriaRequestDto>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Categoria)
                .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
                .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");

            RuleFor(x => x.Descripcion)
                .NotNull().WithMessage("El campo DESCRIPCION no puede ser nulo")
                .NotEmpty().WithMessage("El campo DESCRIPCION no puede estar vacio");
        }
    }
}
