using Fluid;
using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using System;
using System.Threading.Tasks;

namespace Nca.Library.Repositories.Reporting
{
    internal class FluidHtmlRenderTask : IHtmlRenderTask
    {
        public const string _MODEL_ = "model";
        private readonly ILogger _logger;

        public FluidHtmlRenderTask(IReportContext reportContext)
        {
            _logger = reportContext.LoggerFactory.CreateLogger<FluidHtmlRenderTask>();
        }
        public async Task<string> RenderAsync(IReportingModel model, string template)
        {
            try
            {
                template = ReplaceHeaderFooter(model, template);
                if (FluidTemplate.TryParse(template, out var templateResult))
                {
                    var context = new TemplateContext();
                    context.MemberAccessStrategy.Register(model.Data.GetType());
                    context.SetValue(_MODEL_, model.Data);
                    return await templateResult.RenderAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error html parser", ex);
            }
            return string.Empty;
        }

        private string ReplaceHeaderFooter(IReportingModel model, string template)
        {
            if (!string.IsNullOrEmpty(model.Header))
            {
                template = template.Replace("_HEADER_", model.Header);
            }
            if (!string.IsNullOrEmpty(model.Footer))
            {
                template =  template.Replace("_FOOTER_", model.Footer);
            }
            return template;
        }
    }
}
