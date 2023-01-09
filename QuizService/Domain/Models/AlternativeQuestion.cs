using Domain.Exceptions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;

namespace Domain.Models;

public class AlternativeQuestion
{
    [JsonProperty] 
    public Guid QuestionId { get; private set; }
    
    [JsonProperty]
    public string Question { get; private set;}
    
    [JsonProperty]
    public List<QuestionAlternative> QuestionAlternatives { get; private set;}

    
    [JsonConstructor]
    protected AlternativeQuestion(){}
    private AlternativeQuestion(Guid questionId, string question, List<QuestionAlternative> alternatives)
    {
        QuestionId = questionId;
        Question = question ?? throw new ArgumentNullException(nameof(question));
        QuestionAlternatives = alternatives ?? throw new ArgumentNullException(nameof(alternatives));

        var isValid = IsValid(question, alternatives);
        if (isValid.Item1 is false)
        {
            throw new ValidationException(isValid.Item2);
        }
    }

    public static AlternativeQuestion Create(string question, List<QuestionAlternative> alternatives)
    {
        var questionId = Guid.NewGuid();
        return new AlternativeQuestion(questionId, question, alternatives);
    }

    public static (bool, string) IsValid(string question, List<QuestionAlternative> alternatives)
    {
        if (question is null || alternatives is null || alternatives.Count == 0)
        {
            return (false, "Null or empty values");
        }
        
        if (alternatives.Any(q => q.IsCorrect) is false)
        {
            return (false, "A question with alternatives must have at least one correct answer");
        }

        return (true, string.Empty);
    }
    
}
public class QuestionAlternative
{
    [JsonProperty] public string Alternative { get; private set; }
    [JsonProperty] public bool IsCorrect { get; private set; }
    
    [JsonConstructor]
    protected QuestionAlternative() {}

    public QuestionAlternative(string alternative, bool isCorrect)
    {
        Alternative = alternative ?? throw new ArgumentNullException(nameof(alternative));
        IsCorrect = isCorrect;
    }
}