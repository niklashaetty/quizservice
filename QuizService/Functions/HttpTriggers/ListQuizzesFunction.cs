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

public class ListQuizzesFunction
{
    private readonly IQuizRepository _quizRepository;

    public ListQuizzesFunction(IQuizRepository quizRepository)
    {
        ArgumentNullException.ThrowIfNull(quizRepository);
        _quizRepository = quizRepository;
    }

    [OpenApiOperation(operationId: "ListQuizzes", tags: new[] {"Quiz"}, Summary = "List all quizzes that exist")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(List<SimpleQuizResponse>))]
    [FunctionName("ListQuizzesFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "quizzes")] 
        HttpRequest req,
        ILogger log)
    {
        var quiz = await _quizRepository.ListAll();
        return new OkObjectResult(QuizResponseMapper.ToSimpleQuizResponse(quiz));
    }
}