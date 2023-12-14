using FluentValidation;
using ShoeStore.Application.Dtos.Producto.Request;

namespace ShoeStore.Application.Validators.Producto
{
    public class ProductoValidator : AbstractValidator<ProductoRequestDto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Producto)
               .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
               .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");

            RuleFor(x => x.Descripcion)
               .NotNull().WithMessage("El campo DESCRIPCION no puede ser nulo")
               .NotEmpty().WithMessage("El campo DESCRIPCION no puede estar vacio");

            RuleFor(x => x.Precio)
               .NotNull().WithMessage("El campo PRECIO no puede ser nulo")
               .NotEmpty().WithMessage("El campo PRECIO no puede estar vacio");
        }
    }
}
