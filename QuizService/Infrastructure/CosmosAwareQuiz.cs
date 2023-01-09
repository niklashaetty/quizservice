using Domain.Models;
using Newtonsoft.Json;

namespace Infrastructure;

public class CosmosAwareQuiz : Quiz
{
    [JsonProperty(PropertyName = "_etag")] 
    public string ETag { get; set; }
    
    [JsonProperty(PropertyName = "id")] 
    public string Id { get; set; } 
    
    public CosmosAwareQuiz(Guid quizId, string quizName, List<AlternativeQuestion> questions) : base(quizId, quizName, questions)
    {
        Id = QuizId.ToString();
    }

    public static CosmosAwareQuiz ToCosmosAware(Quiz quiz)
    {
        return new CosmosAwareQuiz(quiz.QuizId, quiz.QuizName, quiz.AlternativeQuestions);
    }
}