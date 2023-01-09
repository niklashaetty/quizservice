using Domain.Models;

namespace Domain.Services.QuestionCreationService;

public interface IAddQuestionService
{
    public Task<Quiz> AddAlternativeQuestion(Guid quizId, AlternativeQuestion alternativeQuestion);
}