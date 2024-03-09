using ApiJuegos.DTOs;
using FluentValidation;

namespace ApiJuegos.Validaciones
{
    public class CrearComentarioDTOValidator : AbstractValidator<CrearComentarioDTO>
    {

        public CrearComentarioDTOValidator()
        {
            RuleFor(c => c.Cuerpo).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS)
                .MaximumLength(1000).WithMessage(Utilidades.MaxLengthMS);
        }
    }
}
