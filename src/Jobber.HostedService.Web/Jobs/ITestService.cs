using Jobber.HostedService.Web.Model;

namespace Jobber.HostedService.Web.Jobs;


public interface ITestService
{
    public bool RunTests(TestType testType);
}