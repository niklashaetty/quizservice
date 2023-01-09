using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Api;
using Domain.Services.QuizCreationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
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
    [OpenApiRequestBody(MediaTypeNames.Application.Json, typeof(CreateQuizRequest))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, MediaTypeNames.Application.Json, typeof(SimpleQuizResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Invalid request")]
    [FunctionName("CreateQuizFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "quizzes")] 
        HttpRequest req, 
        ILogger log)
    {
        log.LogInformation("Creating a new quiz");
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var createQuizRequest = JsonConvert.DeserializeObject<CreateQuizRequest>(body);
        
        var quiz = await _quizCreationService.Create(createQuizRequest?.QuizName);
        return new CreatedResult($"/api/quizzes/{quiz.QuizId}", new SimpleQuizResponse(quiz.QuizId, quiz.QuizName));
    }
}