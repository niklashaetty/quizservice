using System.Collections.Generic;
using System.Linq;
using Api;
using Domain.Models;

namespace Functions.Mappers;

public static class QuizResponseMapper
{
    public static QuizResponse ToQuizResponse(Quiz quiz)
    {
        var response = new QuizResponse();
        response.QuizId = quiz.QuizId;
        response.AlternativeQuestions = new List<AlternativeQuestionResponse>();

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