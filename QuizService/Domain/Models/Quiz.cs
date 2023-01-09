using Newtonsoft.Json;

namespace Domain.Models;

public class Quiz
{
    [JsonProperty] public Guid QuizId { get; private set; }
    [JsonProperty] public List<AlternativeQuestion> AlternativeQuestions { get; private set; } = new List<AlternativeQuestion>();

    public static Quiz Create()
    {
        return new Quiz(Guid.NewGuid(), new List<AlternativeQuestion>());
    }
    
    [JsonConstructor]
    protected Quiz()
    {
    }
    
    public Quiz(Guid quizId, List<AlternativeQuestion> questions)
    {
        QuizId = quizId;
        AlternativeQuestions = questions;
    }

    public void AddAlternativeQuestion(AlternativeQuestion alternativeQuestion)
    {
        AlternativeQuestions.Add(alternativeQuestion);
    }
}