using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using Nca.Library.Models.Reporting;

namespace Nca.Library.Repositories.Reporting
{
    public class ReportingFactory : IReportingFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public ReportingFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public IHtmlRenderTask CreateCleanupHtmlRenderTask(IReportContext reportContext)
        {
            return new CleanupHtmlRenderTask(reportContext);
        }

        public IHtmlRenderTask CreateFluidHtmlRenderTask(IReportContext reportContext)
        {
            return new FluidHtmlRenderTask(reportContext);
        }
        public IReportContext CreateReportContext(string tempPath, string templatePath)
        {
            return new ReportContext(_loggerFactory, tempPath, templatePath);
        }

        public IReportingModel CreateReportingModel(string headerTemplate, string footerTemplate, object data)
        {
            return new ReportingModel(headerTemplate, footerTemplate, data);
        }
    }
}
