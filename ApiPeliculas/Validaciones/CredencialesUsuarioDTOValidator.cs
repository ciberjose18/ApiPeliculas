using ApiJuegos.DTOs;
using FluentValidation;

namespace ApiJuegos.Validaciones
{
    public class CredencialesUsuarioDTOValidator : AbstractValidator<CredencialesUsuarioDTO>
    {
        public CredencialesUsuarioDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS).MaximumLength(256)
                .WithMessage(Utilidades.MaxLengthMS).EmailAddress().WithMessage(Utilidades.EmailMS); //El correo electrónico debe ser una dirección de correo electrónico válida


            RuleFor(x => x.Password).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS);
        }
    }
}
