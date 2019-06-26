using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using System.IO;
using System.Threading.Tasks;

namespace Nca.Library.Repositories.Reporting
{
    internal class ReportingRepository : IReportingRepository
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IReportingFactory _reportingFactory;
        private readonly IReportingSettings _reportingSettings;

        public ReportingRepository(
            ILoggerFactory loggerFactory,
            IReportingFactory reportingFactory, 
            IReportingSettings reportingSettings)
        {
            _loggerFactory = loggerFactory;
            _reportingFactory = reportingFactory;
            _reportingSettings = reportingSettings;
        }
        public async Task<string> GenerateHtmlReportAsync(string reportTemplate, object model)
        {
            var htmlBody = File.ReadAllText(Path.Combine(_reportingSettings.TempPathDirectory, reportTemplate));
            var htmlHeader = File.ReadAllText(Path.Combine(_reportingSettings.TempPathDirectory, _reportingSettings.HeaderTemplate));
            var htmlFooter = File.ReadAllText(Path.Combine(_reportingSettings.TempPathDirectory, _reportingSettings.FooterTemplate));

            var reportModel = _reportingFactory.CreateReportingModel(htmlHeader, htmlFooter, model);

            using (var reportContext = _reportingFactory.CreateReportContext(_reportingSettings.OutPutDirectory, _reportingSettings.TempPathDirectory))
            {
                IHtmlRenderTask viewEngine = _reportingFactory.CreateFluidHtmlRenderTask(reportContext);
                IHtmlRenderTask emailEngine = _reportingFactory.CreateCleanupHtmlRenderTask(reportContext);

                var body = await viewEngine.RenderAsync(reportModel, htmlBody);
                return await emailEngine.RenderAsync(reportModel, body);
            }
        }
    }
}
