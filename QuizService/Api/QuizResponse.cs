using Api.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class QuizResponse
{
    [JsonRequired]
    [OpenApiProperty(Description = "The quiz identifier")]
    public Guid QuizId { get; set; }
    
    [JsonRequired]
    [OpenApiProperty(Description = "The questions with alternative answer. At least one answer must be correct")]
    public List<AlternativeQuestionResponse> AlternativeQuestions { get; set; } = new List<AlternativeQuestionResponse>();

}