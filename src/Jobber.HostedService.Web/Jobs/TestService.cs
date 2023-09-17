
namespace Jobber.HostedService.Web.Jobs;


public class TestService : BackgroundService, ITestService
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
        Thread.Sleep(3000);
        // ...

        _logger.LogInformation($"{DateTime.Now} RunTests is finished");
        
        return true;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            RunTests();
        }
    }
}