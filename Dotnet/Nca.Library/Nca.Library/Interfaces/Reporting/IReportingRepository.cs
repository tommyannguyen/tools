using System.Threading.Tasks;

namespace Nca.Library.Interfaces.Reporting
{
    public interface IReportingRepository
    {
        Task<string> GenerateHtmlReportAsync(string reportTemplate, object model);
    }
}
