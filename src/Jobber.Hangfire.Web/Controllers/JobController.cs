using Hangfire;
using Jobber.Hangfire.Web.Jobs;
using Jobber.Hangfire.Web.Model;
using Microsoft.AspNetCore.Mvc;
using TimeZoneConverter;

namespace Jobber.Hangfire.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    [HttpGet("/run")]
    public IActionResult Run()
    {
        var jobId = BackgroundJob.Enqueue<ITestService>(x => x.RunTests(Guid.NewGuid(), TestType.OnDemand, CancellationToken.None));

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
        BackgroundJob.ContinueJobWith<ITestService>(jobId, x => x.RunTests(Guid.NewGuid(), TestType.OnDemand, CancellationToken.None));

        return Ok("Continued");
    }

    [HttpGet("/reschedule")]
    public IActionResult Reschedule(string cron)
    {
        var recurringJobOptions = new RecurringJobOptions
        {
            TimeZone = TZConvert.GetTimeZoneInfo("Etc/GMT+3")
        };
        RecurringJob.AddOrUpdate<ITestService>("id-run-and-wait", x => x.RunTests(Guid.NewGuid(), TestType.Recurring, CancellationToken.None), cron, recurringJobOptions);
        return Ok("Rescheduled");
    }

    [HttpGet("/deschedule")]
    public IActionResult Deschedule(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            id = "id-run-and-wait";
        }

        RecurringJob.RemoveIfExists(id);
        return Ok("Descheduled");
    }

    [HttpGet("/trigger")]
    public IActionResult Trigger(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            id = "id-run-and-wait";
        }

        RecurringJob.TriggerJob(id);
        return Ok("Triggered");
    }
}
