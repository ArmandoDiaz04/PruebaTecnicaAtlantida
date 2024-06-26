namespace API.Core.Application.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException() : base("La acción que intentas ejecutar está prohibida")
        {

        }

        public ForbiddenException(string msg) : base(msg)
        {

        }
    }
}
