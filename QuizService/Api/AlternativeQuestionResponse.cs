using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class AlternativeQuestionResponse : AlternativeQuestionRequest
{
    [JsonRequired]
    [OpenApiProperty(Description = "The question identifier")]
    public Guid QuestionId { get; set; }
}