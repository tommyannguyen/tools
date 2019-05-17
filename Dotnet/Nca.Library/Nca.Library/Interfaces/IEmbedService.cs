using Nca.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nca.Library.Interfaces
{
    public interface IEmbedService
    {
        Task<IEnumerable<ReportInfo>> GetReports();
        Task<EmbedReport> GetEmbedReport(ReportId reportId);
        Task<EmbedReport> GetEmbedReport(ReportGroup reportGroup);
    }
}
