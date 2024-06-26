namespace API.MiddlewareException.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
        public CodeErrorException(int statusCode, string? message = null, string? details = null, IDictionary<string, string[]>? errors = null) : base(statusCode, message)
        {
            Details = details;
            Errors = errors;
        }
    }
}
