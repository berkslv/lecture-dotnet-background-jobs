using Jobber.TaskRun.Web.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Jobber.TaskRun.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly ITestService _testService;

    public JobController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Task.Run(() => {
            _testService.RunTests();
        });

        return Ok("Ok");
    }
}
