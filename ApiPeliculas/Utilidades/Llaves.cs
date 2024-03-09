using Microsoft.IdentityModel.Tokens;

namespace ApiJuegos.Utilidades
{
    public static class Llaves
    {
        // Esta línea define el nombre de nuestra aplicación.
        public const string IssuerPropio = "nuestra-app";
        // Estas líneas definen dónde el autenticador puede encontrar las llaves.
        // Es como si le dijéramos dónde guardamos las llaves de nuestra casa.
        private const string SeccionLlaves = "Authentication:Schemes:Bearer:SigningKeys";
        private const string SeccionLlaves_Emisor = "Issuer";
        private const string SeccionLlaves_Valor = "Value";
        // Este método es como una instrucción para el autenticador sobre cómo encontrar la llave correcta.
        public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration) => ObtenerLlave(configuration, IssuerPropio);
        // Este método le dice al autenticador cómo encontrar la llave correcta para un jugador específico.
        public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration, string issuer)
        {
            // Aquí, el autenticador busca la llave correcta.
            var signingKey = configuration.GetSection(SeccionLlaves).GetChildren()
               .SingleOrDefault(llave => llave[SeccionLlaves_Emisor] == issuer);
            // Si el autenticador encuentra la llave correcta, la utiliza para verificar que el jugador es quien dice ser.
            if (signingKey is not null && signingKey[SeccionLlaves_Valor] is string valorLlave)
            {
                yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
            }
        }
        // Este método le dice al autenticador cómo encontrar todas las llaves. Es como si le dijéramos dónde están todas las llaves de todas las casas en nuestra ciudad.
        public static IEnumerable<SecurityKey> ObtenerTodasLasLlaves(IConfiguration configuration)
        {
            // Aquí, el guardián busca todas las llaves.
            var signingKeys = configuration.GetSection(SeccionLlaves).GetChildren();
            // Para cada llave que encuentra, la utiliza para verificar que el jugador es quien dice ser.
            foreach (var signingKey in signingKeys)
            {
                if (signingKey[SeccionLlaves_Valor] is string valorLlave)
                {
                    yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
                }
            }
        }
    }
}
