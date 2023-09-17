namespace Jobber.Hangfire.Web.Jobs;


public interface ITestService
{
    public bool RunTests(CancellationToken cancellationToken);
}