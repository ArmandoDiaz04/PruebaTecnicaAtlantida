namespace API.Core.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"La entidad {name}, con el filtro ({key}) no fue encontrado")
        {
        }
    }
}
