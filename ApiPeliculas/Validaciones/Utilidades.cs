using System.Runtime.ConstrainedExecution;

namespace ApiJuegos.Validaciones
{
    public static class Utilidades
    {
        public static string CampoRequeridoMS = "El campo {PropertyName} es requerido";
        public static string MaxLengthMS = "El campo {PropertyName} no debe ser mayor a {MaxLength} caracteres";
        public static string PrimeraLetraMayusculaMS = "La primera letra de {PropertyName} debe ser mayúscula";
        public static string EmailMS = "El campo {PropertyName} debe ser un email válido";

        public static string GreaterThanMS(DateTime fechaMin)
        {
            return "La fecha de nacimiento debe ser mayor a " + fechaMin.ToString("yyyy - MM - dd");
        }

        public static bool PrimeraLetraMayuscula(string nombre)
        {
            // Primero, verifica si el string nombre está vacío o solo contiene espacios en blanco.
            // Si es así, el método devuelve true y termina.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return true;
            }
            // Si el string nombre no está vacío y no solo contiene espacios en blanco,
            // el método verifica si el primer carácter del string es una letra mayúscula.
            // Char.IsUpper es un método de la clase Char que devuelve true si el carácter proporcionado es una letra mayúscula.
            // En este caso, estamos pasando el primer carácter del string nombre al método Char.IsUpper.
            // Si el primer carácter es una letra mayúscula, el método devuelve true. De lo contrario, devuelve false.
            return Char.IsUpper(nombre[0]);
        }

    }
}
    