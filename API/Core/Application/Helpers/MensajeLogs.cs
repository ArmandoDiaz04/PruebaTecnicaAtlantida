namespace API.Core.Application.Helpers
{
    public class MensajeLogs
    {
        public static void InicioEjecucion<T>(ILogger<T> logger, string nameMethod) where T : class
        {
            logger.LogInformation($"Inicio la ejecución del metodo {nameMethod}");
        }

        public static void FinalizoEjecucion<T>(ILogger<T> logger, string nameMethod) where T : class
        {
            logger.LogInformation($"Finalizo la ejecución del metodo {nameMethod}");
        }
    }
}
