using Domain.Models;

namespace Domain.Services.QuizCreationService;

public interface IQuizCreationService
{
    public Task<Quiz> Create();
}