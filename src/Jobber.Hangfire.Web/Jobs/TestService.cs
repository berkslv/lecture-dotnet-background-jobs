
namespace Jobber.Hangfire.Web.Jobs;


public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }

    public bool RunTests(CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        _logger.LogInformation($"{DateTime.Now} RunTests is started. Id: {id}");

        // ...
        Thread.Sleep(20000);
        ThrowRandomly();
        // ...

        _logger.LogInformation($"{DateTime.Now} RunTests is finished. Id: {id}");
        return true;
    }

    private void ThrowRandomly() 
    {
        var random = new Random();
        var number = random.Next(1, 3);

        if (number == 2)
        {
            _logger.LogError($"{DateTime.Now} RunTests is failed!");
            throw new Exception("Unexpected and unhandled exception is throwed!");
        }
    }
}