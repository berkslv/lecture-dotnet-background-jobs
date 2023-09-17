using Hangfire;
using Jobber.Hangfire.Web.Jobs;
using Microsoft.AspNetCore.Mvc;
using TimeZoneConverter;

namespace Jobber.Hangfire.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{

    public JobController()
    {
    }

    [HttpGet("/run")]
    public IActionResult Run()
    {
        var jobId = BackgroundJob.Enqueue<ITestService>(x => x.RunTests(CancellationToken.None));

        return Ok(jobId);
    }

    [HttpGet("/stop")]
    public IActionResult Stop(string jobId)
    {
        BackgroundJob.Delete(jobId);

        return Ok("Stopped");
    }

    [HttpGet("/continue")]
    public IActionResult Continue(string jobId)
    {
        BackgroundJob.ContinueJobWith<ITestService>(jobId, x => x.RunTests(CancellationToken.None));

        return Ok("Continued");
    }

    [HttpGet("/reschedule")]
    public IActionResult Reschedule(string cron)
    {
        var timeZone = TZConvert.GetTimeZoneInfo("Etc/GMT+3");
        RecurringJob.AddOrUpdate<ITestService>(x => x.RunTests(CancellationToken.None), cron, timeZone);

        return Ok("Rescheduled");
    }
}
