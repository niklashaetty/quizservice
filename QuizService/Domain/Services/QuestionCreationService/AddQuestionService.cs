using Domain.Models;
using Domain.Repositories;

namespace Domain.Services.QuestionCreationService;

public class AddQuestionService : IAddQuestionService
{
    private readonly IQuizRepository _quizRepository;

    public AddQuestionService(IQuizRepository quizRepository)
    {
        ArgumentNullException.ThrowIfNull(quizRepository);
        _quizRepository = quizRepository;
    }

    public async Task<Quiz> AddAlternativeQuestion(Guid quizId, AlternativeQuestion alternativeQuestion)
    {
        ArgumentNullException.ThrowIfNull(alternativeQuestion);
        var quiz = await _quizRepository.Get(quizId);
        
        quiz.AddAlternativeQuestion(alternativeQuestion);
        return await _quizRepository.PersistUpdate(quiz);
    }
}