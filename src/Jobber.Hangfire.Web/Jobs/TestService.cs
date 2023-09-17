
using System.Globalization;

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

        try
        {
            _logger.LogInformation($"{DateTime.Now} RunTests is started. Id: {id}");

            cancellationToken.ThrowIfCancellationRequested();
            // ...
            Thread.Sleep(20000);
            ThrowRandomly();
            // ...

            _logger.LogInformation($"{DateTime.Now} RunTests is finished. Id: {id}");
            return true;  
        }
        catch (OperationCanceledException exception)
        {
            _logger.LogError($"{DateTime.Now} RunTests is failed. Exception: {exception.Message} Id: {id}");
        }
        catch(Exception exception)
        {
            _logger.LogError($"{DateTime.Now} RunTests is failed. Exception: {exception.Message} Id: {id}");
        }
        
        return false;
    }

    private void ThrowRandomly() 
    {
        var random = new Random();
        var number = random.Next(1, 3);

        if (number == 2)
        {
            throw new Exception("Unexpected and unhandled exception is throwed!");
        }
    }
}