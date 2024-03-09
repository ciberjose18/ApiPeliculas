namespace ApiJuegos.Entidades
{
    public class Error
    {
        // Id es una propiedad de tipo Guid. 
        // Guid es una abreviatura de 'Globally Unique Identifier', es decir, un identificador único a nivel global.
        // Cada instancia de Error tendrá un Id único.
        public Guid Id { get; set; }
        // MensajeDeError es una propiedad de tipo string que puede ser nula.
        // Esta propiedad se utiliza para almacenar el mensaje de error.
        public string? MensajeDeError { get; set; }
        // Esta propiedad se utiliza para almacenar la traza de la pila en el momento en que se produjo el error.
        // La traza de la pila es una representación de la pila de llamadas en el momento del error, 
        // que puede ser muy útil para depurar el error.
        public string? StackTrace { get; set; }
        // Esta propiedad se utiliza para almacenar la fecha y hora en que se produjo el error.
        public DateTime Fecha { get; set; }
    }
}
