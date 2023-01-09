using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class AlternativeQuestionRequest
{
    [JsonRequired]
    [OpenApiProperty(Description = "The question", Default = "How long is a rope?")]
    public string Question { get; set; }
    
    [JsonRequired]
    [OpenApiProperty(Description = "The list of alternatives the question should have")]
    public List<QuestionAlternativeDto> Alternatives { get; set; } = new List<QuestionAlternativeDto>();
}

public class QuestionAlternativeDto
{
    [JsonRequired]
    [OpenApiProperty(Description = "An alternative", Default = "Very long")]
    public string Alternative { get; set; }
    
    [JsonRequired]
    [OpenApiProperty(Description = "Indicates if an answer is correct or not", Default = false)]
    public bool IsCorrect { get; set; }
}