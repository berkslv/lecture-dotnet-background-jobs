namespace Jobber.TaskRun.Web.Jobs;


public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }

    public bool RunTests()
    {
        _logger.LogInformation($"{DateTime.Now} RunTests is started");

        // ...
        Thread.Sleep(5000);
        // ...

        _logger.LogInformation($"{DateTime.Now} RunTests is finished");
        return true;
    }
}