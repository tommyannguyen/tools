namespace Nca.Library.Interfaces.Reporting
{
    public interface IReportingFactory
    {
        IReportingModel CreateReportingModel(string headerTemplate, string footerTemplate, object data);
        IHtmlRenderTask CreateCleanupHtmlRenderTask(IReportContext reportContext);
        IHtmlRenderTask CreateFluidHtmlRenderTask(IReportContext reportContext);
    }
}
