using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Api;
using Api.Enums;
using Domain;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.QuestionCreationService;
using Domain.Services.QuizCreationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Functions.HttpTriggers;

public class AddQuestionFunction
{
    private readonly IAddQuestionService _addQuestionService;

    public AddQuestionFunction(IAddQuestionService addQuestionService)
    {
        ArgumentNullException.ThrowIfNull(addQuestionService);
        _addQuestionService = addQuestionService;
    }

    #region Open Api Definition

    [OpenApiOperation(operationId: "AddQuestion", tags: new[] {"Quiz"}, Summary = "Add a question")]
    [OpenApiRequestBody(MediaTypeNames.Application.Json, typeof(QuestionDto))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NoContent)]
    [OpenApiResponseWithBody(HttpStatusCode.NotFound, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Cannot find quiz with given quiz id")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Invalid request")]
    [OpenApiParameter("quizId",
        In = ParameterLocation.Path,
        Required = true,
        Type = typeof(Guid),
        Summary = "The quiz identifier")]
    #endregion

    [FunctionName("AddQuestionFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "quizzes/{quizId}/questions")]
        HttpRequest req,
        Guid quizId,
        ILogger log)
    {
        log.LogInformation("Creating a new quiz");

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var questionDto = JsonConvert.DeserializeObject<QuestionDto>(body);
        if (questionDto.Type is not QuestionType.Alternative)
        {
            return new BadRequestObjectResult("Only alternative question types are supported");
        }

        if (questionDto.AlternativeQuestion is null)
        {
            return new BadRequestObjectResult("Missing question!");
        }

        var alternatives =
            questionDto.AlternativeQuestion.Alternatives.Select(
                q => new QuestionAlternative(q.Alternative, q.IsCorrect)).ToList();
        
        var question = AlternativeQuestion.Create(questionDto.AlternativeQuestion.Question, alternatives);
        await _addQuestionService.AddAlternativeQuestion(quizId, question);
        return new NoContentResult();
    }
}