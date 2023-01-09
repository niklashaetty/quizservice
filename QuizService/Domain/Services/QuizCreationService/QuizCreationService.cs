using Domain.Models;
using Domain.Repositories;

namespace Domain.Services.QuizCreationService;

public class QuizCreationService : IQuizCreationService
{
    private readonly IQuizRepository _quizRepository;
    
    public QuizCreationService(IQuizRepository quizRepository)
    {
        ArgumentNullException.ThrowIfNull(quizRepository);
        _quizRepository = quizRepository;
    }

    public async Task<Quiz> Create()
    {
        var newQuiz = Quiz.Create();
        return await _quizRepository.Add(newQuiz);
    }
}