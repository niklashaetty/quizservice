using Api.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class QuestionDto
{
    [JsonRequired]
    [OpenApiProperty(Description = "Type of the question", Default = QuestionType.Alternative)]
    public QuestionType Type { get; set; }
    
    [OpenApiProperty(Description = "The question. Required if type is set to Alternative")]
    public AlternativeQuestionRequest AlternativeQuestion { get; set; }
}