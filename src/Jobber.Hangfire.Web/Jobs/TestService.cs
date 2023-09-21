
using System.Globalization;
using Jobber.Hangfire.Web.Model;

namespace Jobber.Hangfire.Web.Jobs;


public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }

    public bool RunTests(Guid id, TestType testType, CancellationToken cancellationToken)
    {
        var type = Enum.GetName(typeof(TestType), testType);

        try
        {
            _logger.LogInformation($"{DateTime.Now} {type} RunTests is started. Id: {id}");

            cancellationToken.ThrowIfCancellationRequested();
            // ...
            Thread.Sleep(5000);
            ThrowRandomly();
            // ...

            _logger.LogInformation($"{DateTime.Now} {type} RunTests is finished. Id: {id}");
            return true;  
        }
        catch (OperationCanceledException exception)
        {
            _logger.LogError($"{DateTime.Now} {type} RunTests is failed. Exception: {exception.Message} Id: {id}");
            throw;
        }
        catch(Exception exception)
        {
            _logger.LogError($"{DateTime.Now} {type} RunTests is failed. Exception: {exception.Message} Id: {id}");
            throw;
        }
    }

    private void ThrowRandomly() 
    {
        var random = new Random();
        var number = random.Next(1, 3);

        if (number == 2)
        {
            throw new Exception("Error is throwed!");
        }
    }
}