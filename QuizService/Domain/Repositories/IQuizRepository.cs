using Domain.Models;

namespace Domain.Repositories;

public interface IQuizRepository
{
    public Task<Quiz> Add(Quiz quiz);
    
    /// <summary>
    /// Gets a quiz
    /// </summary>
    /// <throws>QuizNotFoundException</throws>
    public Task<Quiz> Get(Guid quizId);

    /// <summary>
    /// Persist a quiz update
    /// </summary>
    /// <throws>QuizNotFoundException</throws>
    public Task<Quiz> PersistUpdate(Quiz updatedQuiz);
}