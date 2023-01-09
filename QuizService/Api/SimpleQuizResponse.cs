using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class SimpleQuizResponse
{
    [JsonRequired]
    [OpenApiProperty(Description = "The quiz identifier")]
    public Guid QuizId { get; set; }
    
    [JsonRequired]
    [OpenApiProperty(Description = "The name of the quiz. ", 
        Default = "My super awesome quiz")]
    public string QuizName { get; set; }

    public SimpleQuizResponse(Guid quizId, string quizName)
    {
        ArgumentNullException.ThrowIfNull(quizName);
        QuizId = quizId;
        QuizName = quizName;
    }
}