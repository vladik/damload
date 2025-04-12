using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace DamLoad.Api.Exceptions
{
    public class ExceptionResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = "An error occurred.";

        [JsonPropertyName("errors")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, List<string>>? Errors { get; set; }

        // Default constructor
        public ExceptionResponse() { }

        // For validation failures
        public ExceptionResponse(List<ValidationFailure> failures, int statusCode = 400)
        {
            StatusCode = statusCode;
            Message = "Validation failed.";
            Errors = failures
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.ErrorMessage).ToList()
                );
        }
    }

}
