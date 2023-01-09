namespace Api;

public class CreateQuizResponse
{
    public Guid QuizId { get; set; }

    public CreateQuizResponse(Guid quizId)
    {
        QuizId = quizId;
    }
}