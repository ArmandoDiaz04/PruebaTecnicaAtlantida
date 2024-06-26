using API.Core.Application.Exceptions;
using API.MiddlewareException.Errors;
using Newtonsoft.Json;
using System.Net;

namespace API.MiddlewareException
{
    public class MiddlewareHandlerException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareHandlerException> _logger;
        private readonly IHostEnvironment _env;

        public MiddlewareHandlerException(RequestDelegate next, ILogger<MiddlewareHandlerException> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var jsonErrors = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, jsonErrors, validationException.Errors));
                        break;

                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ForbiddenException forbiddenException:
                        statusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        break;
                }


                context.Response.StatusCode = statusCode;

                if (string.IsNullOrEmpty(result))
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));

                await context.Response.WriteAsync(result);

            }
        }

    }
}
