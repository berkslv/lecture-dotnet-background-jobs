using Jobber.Hangfire.Web.Model;

namespace Jobber.Hangfire.Web.Jobs;


public interface ITestService
{
    public bool RunTests(Guid id, TestType Type, CancellationToken cancellationToken);
}