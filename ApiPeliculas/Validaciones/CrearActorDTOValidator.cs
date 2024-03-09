using ApiJuegos.DTOs;
using FluentValidation;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace ApiJuegos.Validaciones
{
    public class CrearActorDTOValidator : AbstractValidator<CrearActorDTO>
    {

        public CrearActorDTOValidator()
        {
            RuleFor(a => a.Nombre).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS)
                .MaximumLength(150).WithMessage(Utilidades.MaxLengthMS);

            // La regla dice que la FechaNacimiento debe ser mayor que 01 / 01 / 1900(GreaterThan).
            var fechaMin = new System.DateTime(1900, 1, 1);
            RuleFor(a => a.FechaNacimiento).GreaterThan(fechaMin).WithMessage(Utilidades.GreaterThanMS(fechaMin));


                


        }

    }
}
