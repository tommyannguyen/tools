using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using Nca.Library.Repositories.Reporting;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nca.Test
{
    public class ReportingTest
    {
        private readonly ILoggerFactory _loggerFactory;
        public ReportingTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

            _loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        }
        [Test]
        public async Task TestReportAsync()
        {
            var outPutDirectory = "Data\\Temp";
            var tempPathDirectory = "Data\\Templates";
            var model = new { Name = "Con bướm xinh", Job = 100 };

            string headerTemplate = "header.html";
            string bodyTemplate = "email.html";
            string footerTemplate = "footer.html";

            var htmlBody = await File.ReadAllTextAsync(Path.Combine(tempPathDirectory, bodyTemplate));
            var htmlHeader = await File.ReadAllTextAsync(Path.Combine(tempPathDirectory, headerTemplate)); 
            var htmlFooter = await File.ReadAllTextAsync(Path.Combine(tempPathDirectory, footerTemplate)); 


            var reportingFactory = new ReportingFactory(_loggerFactory);
            var reportModel = reportingFactory.CreateReportingModel(htmlHeader, htmlFooter, model);
            // factory return context
            using (var reportContext = reportingFactory.CreateReportContext(outPutDirectory, tempPathDirectory))
            {
                IHtmlRenderTask viewEngine = reportingFactory.CreateFluidHtmlRenderTask(reportContext);
                IHtmlRenderTask preMailerEngine = reportingFactory.CreateCleanupHtmlRenderTask(reportContext);
                
                var body = await viewEngine.RenderAsync(reportModel, htmlBody);
                body = await preMailerEngine.RenderAsync(reportModel, body);

                File.WriteAllBytes(Path.Combine(outPutDirectory, "tententen.html"), Encoding.UTF8.GetBytes(body));
            }
            Assert.True(true);
        }



    }
}
