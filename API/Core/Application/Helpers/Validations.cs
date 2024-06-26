using System.Text.RegularExpressions;

namespace API.Core.Application.Helpers
{
    public class Validations
    {
        public static bool MayorQueCero(int id) => id > 0;
        public static bool EsFechaMenorAlaFechaActual(DateTime date)
        {
            bool result = date.Date < DateTime.Now.Date;
            return !result;
        }
        public static bool EsHoraMenorAlaHoraActual(TimeSpan hour) => hour < DateTime.Now.TimeOfDay;
        public static bool ContieneMayuscula(string texto) => Regex.IsMatch(texto, @"[A-Z]");
        public static bool ContieneMinuscula(string texto) => Regex.IsMatch(texto, @"[a-z]");
        public static bool ContieneCaracterEspecial(string texto) => Regex.IsMatch(texto, @"[^a-zA-Z0-9]");
    }
}
