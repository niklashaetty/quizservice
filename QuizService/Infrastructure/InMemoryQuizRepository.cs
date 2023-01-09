using Domain;
using Domain.Models;
using Domain.Repositories;
using Newtonsoft.Json;

namespace Infrastructure;

public class InMemoryQuizRepository : IQuizRepository
{
    private Dictionary<Guid, TestableQuiz> Quizzes { get; } = new Dictionary<Guid, TestableQuiz>();
    
    public Task<Quiz> Add(Quiz quiz)
    {
        var testableQuiz = new TestableQuiz(quiz);
        Quizzes.Add(quiz.QuizId, testableQuiz);
        return Task.FromResult(testableQuiz.Deserialize());
    }

    public Task<Quiz> Get(Guid quizId)
    {
        if (Quizzes.ContainsKey(quizId) is false)
        {
            throw new QuizNotFoundException(quizId);
        }

        return Task.FromResult(Quizzes[quizId].Deserialize());
    }

    public Task<Quiz> PersistUpdate(Quiz updatedQuiz)
    {
        if (Quizzes.ContainsKey(updatedQuiz.QuizId) is false)
        {
            throw new QuizNotFoundException(updatedQuiz.QuizId);
        }

        Quizzes[updatedQuiz.QuizId] = new TestableQuiz(updatedQuiz);
        return Task.FromResult(Quizzes[updatedQuiz.QuizId].Deserialize());
    }

    public Task<List<Quiz>> ListAll()
    {
        return Task.FromResult(Quizzes.Values.Select(q => q.Deserialize()).ToList());
    }
}

internal class TestableQuiz
{
    public Guid QuizId { get; }
    public string SerializedQuiz { get; }
    
    public TestableQuiz(Quiz quiz)
    {
        QuizId = quiz.QuizId;
        SerializedQuiz = JsonConvert.SerializeObject(quiz);
    }

    public Quiz Deserialize()
    {
        var domain = JsonConvert.DeserializeObject<Quiz>(SerializedQuiz);
        if (domain is null)
        {
            throw new InvalidOperationException("Failed to deserialize into quiz");
        }

        return domain;
    }
}