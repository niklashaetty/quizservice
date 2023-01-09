using System.Collections.Generic;
using System.Linq;
using Api;
using Domain.Models;

namespace Functions.Mappers;

public static class QuizResponseMapper
{
    public static List<SimpleQuizResponse> ToSimpleQuizResponse(List<Quiz> quizzes)
    {
        return quizzes.Select(q => new SimpleQuizResponse(q.QuizId, q.QuizName)).ToList();
    }

    public static DetailedQuizResponse ToDetailedQuizResponse(Quiz quiz)
    {
        var response = new DetailedQuizResponse
        {
            QuizId = quiz.QuizId,
            QuizName = quiz.QuizName,
            AlternativeQuestions = new List<AlternativeQuestionResponse>()
        };

        foreach (var qDto in quiz.AlternativeQuestions.Select(q => new AlternativeQuestionResponse()
                 {
                     QuestionId = q.QuestionId,
                     Question = q.Question,
                     Alternatives = q.QuestionAlternatives.Select(qq => new QuestionAlternativeDto()
                     {
                         Alternative = qq.Alternative,
                         IsCorrect = qq.IsCorrect
                     }).ToList()
                 }))
        {
            response.AlternativeQuestions.Add(qDto);
        }

        return response;
    }
}