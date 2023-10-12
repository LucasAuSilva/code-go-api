
using System.Text.Json.Serialization;

namespace CodeGo.Infrastructure.Http.Judge0Api.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record CreateSubmissionResponse(
    [property: JsonPropertyName("token")] string Token
);

// public record CreateSubmissionResponse(
//     string Token
// );
