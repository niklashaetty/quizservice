using Domain;
using Domain.Models;
using Newtonsoft.Json;

namespace Infrastructure;

public class CosmosAwareQuiz : Quiz
{
    [JsonProperty(PropertyName = "_etag")] 
    public string ETag { get; set; }
    
    [JsonProperty(PropertyName = "id")] 
    public string Id { get; set; } 
    
    public CosmosAwareQuiz(Guid quizId, List<AlternativeQuestion> questions) : base(quizId, questions)
    {
        Id = QuizId.ToString();
    }

    public static CosmosAwareQuiz ToCosmosAware(Quiz quiz)
    {
        return new CosmosAwareQuiz(quiz.QuizId, quiz.AlternativeQuestions);
    }
}