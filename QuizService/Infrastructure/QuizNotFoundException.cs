namespace Infrastructure;

public class QuizNotFoundException : Exception
{
    public QuizNotFoundException(Guid quizId) : base($"Quiz with id {quizId} not found")
    {
    }
}