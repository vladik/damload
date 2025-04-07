using DamLoad.Abstractions.Exceptions;
using DamLoad.Api.Exceptions;
using DamLoad.Data.Database;
using FastEndpoints;
using FluentValidation;
using System.Data.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DamLoad.Api.Processors { 
    public class ExceptionProcessor : IGlobalPostProcessor
    {
        public async Task PostProcessAsync(IPostProcessorContext context, CancellationToken ct)
        {
            if (!context.HasExceptionOccurred)
                return;

            var ex = context.ExceptionDispatchInfo?.SourceException;
            if (ex is null) return;

            var (statusCode, response) = BuildExceptionResponse(ex);

            context.HttpContext.Response.StatusCode = statusCode;
            context.HttpContext.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            await context.HttpContext.Response.WriteAsync(json, ct);
            context.MarkExceptionAsHandled();
        }

        private static (int, ExceptionResponse) BuildExceptionResponse(Exception ex)
        {
            return ex switch
            {
                ValidationException ve => (400, new ExceptionResponse(ve.Errors.ToList())),
                ConflictException => (409, new ExceptionResponse { StatusCode = 409, Message = ex.Message }),
                ArgumentException => (400, new ExceptionResponse { StatusCode = 400, Message = ex.Message }),
                DbException dbEx when DatabaseErrorResolver.IsUniqueViolation(dbEx) =>
                    (409, new ExceptionResponse { StatusCode = 409, Message = "Duplicate record violates a unique constraint." }),
                _ => (500, new ExceptionResponse { StatusCode = 500, Message = "An unexpected error occurred." })
            };
        }
    }
}

