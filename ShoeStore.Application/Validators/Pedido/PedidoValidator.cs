using FluentValidation;
using ShoeStore.Application.Dtos.Pedido.Request;

namespace ShoeStore.Application.Validators.Pedido
{
    public class PedidoValidator : AbstractValidator<PedidoRequestDto>
    {
        public PedidoValidator()
        {
            RuleFor(x => x.Cantidad)
               .NotNull().WithMessage("El campo CANTIDAD no puede ser nulo")
               .NotEmpty().WithMessage("El campo CANTIDAD no puede estar vacio");
        }
    }
}
