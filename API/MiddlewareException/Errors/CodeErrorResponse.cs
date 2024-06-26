﻿namespace API.MiddlewareException.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tienes authorización para este recurso",
                403 => "Acción prohibida",
                404 => "No se encontro el recurso solicitado",
                500 => "Se producieron errores en el servidor",
                _ => string.Empty
            };
        }
    }
}
