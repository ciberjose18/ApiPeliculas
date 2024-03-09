using ApiJuegos.DTOs;
using FluentValidation;

namespace ApiJuegos.Validaciones
{
    public class CrearPeliculaDTOValidator : AbstractValidator<CrearPeliculaDTO>
    {
        public CrearPeliculaDTOValidator()
        {
            RuleFor(p => p.Titulo).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS)
                .MaximumLength(100).WithMessage(Utilidades.MaxLengthMS);

        }
    }
}
