using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Api;
using Domain;
using Domain.Models;
using Domain.Repositories;
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

public class CreateQuizFunction
{
    private readonly IQuizCreationService _quizCreationService;

    public CreateQuizFunction(IQuizCreationService quizCreationService)
    {
        ArgumentNullException.ThrowIfNull(quizCreationService);
        _quizCreationService = quizCreationService;
    }

    [OpenApiOperation(operationId: "CreateQuiz", tags: new[] {"Quiz"}, Summary = "Creates a new empty quiz")]
    [OpenApiResponseWithBody(HttpStatusCode.Created, MediaTypeNames.Application.Json, typeof(CreateQuizResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Invalid request")]
    [FunctionName("CreateQuizFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "quizzes")] 
        HttpRequest req, 
        ILogger log)
    {
        log.LogInformation("Creating a new quiz");
        var quiz = await _quizCreationService.Create();
        return new CreatedResult($"/api/quizzes/{quiz.QuizId}", new CreateQuizResponse(quiz.QuizId));
    }
}