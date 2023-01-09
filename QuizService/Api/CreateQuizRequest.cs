using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Api;

public class CreateQuizRequest
{
    [OpenApiProperty(Description = "The name of the quiz. Will generate a random one if not present in the request", 
        Default = "My super awesome quiz")]
    public string QuizName { get; set; }
}