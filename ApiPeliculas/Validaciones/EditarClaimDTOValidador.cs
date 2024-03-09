using ApiJuegos.DTOs;
using FluentValidation;

namespace ApiJuegos.Validaciones
{
    public class EditarClaimDTOValidador : AbstractValidator<EditarClaimDTO>
    {
        public EditarClaimDTOValidador()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS)
                .MaximumLength(256).WithMessage(Utilidades.MaxLengthMS)
                .EmailAddress().WithMessage(Utilidades.EmailMS);
        }
    }
}
