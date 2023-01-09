using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Api;
using Domain;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.QuizCreationService;
using Functions.Mappers;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.HttpTriggers;

public class GetQuizFunction
{
    private readonly IQuizRepository _quizRepository;

    public GetQuizFunction(IQuizRepository quizRepository)
    {
        ArgumentNullException.ThrowIfNull(quizRepository);
        _quizRepository = quizRepository;
    }

    [OpenApiOperation(operationId: "GetQuiz", tags: new[] {"Quiz"}, Summary = "Get an existing quiz")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(DetailedQuizResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Invalid request")]
    [OpenApiResponseWithBody(HttpStatusCode.NotFound, MediaTypeNames.Application.Json, typeof(ErrorResponse),
        Summary = "Cannot find quiz with given quiz id")]
    [FunctionName("GetQuizFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "quizzes/{quizId}")] 
        HttpRequest req, 
        Guid quizId,
        ILogger log)
    {
        log.LogInformation("Creating a new quiz");
        try
        {
            var quiz = await _quizRepository.Get(quizId);
            return new OkObjectResult(QuizResponseMapper.ToDetailedQuizResponse(quiz));
        }
        catch (QuizNotFoundException e)
        {
            return new NotFoundObjectResult(new ErrorResponse("Not found", "No quiz with this id was found"));
        }
    }
}