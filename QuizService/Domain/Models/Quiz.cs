using Newtonsoft.Json;

namespace Domain.Models;

public class Quiz
{
    [JsonProperty] public Guid QuizId { get; private set; }
    [JsonProperty] public string QuizName { get; private set; }
    [JsonProperty] public List<AlternativeQuestion> AlternativeQuestions { get; private set; } = new List<AlternativeQuestion>();

    public static Quiz Create(string quizName=null)
    {
        quizName ??= Wordlist.GenerateQuizName();
        return new Quiz(Guid.NewGuid(), quizName, new List<AlternativeQuestion>());
    }
    
    [JsonConstructor]
    protected Quiz()
    {
    }
    
    public Quiz(Guid quizId, string quizName, List<AlternativeQuestion> questions)
    {
        QuizId = quizId;
        QuizName = quizName;
        AlternativeQuestions = questions;
    }

    public void AddAlternativeQuestion(AlternativeQuestion alternativeQuestion)
    {
        AlternativeQuestions.Add(alternativeQuestion);
    }
}