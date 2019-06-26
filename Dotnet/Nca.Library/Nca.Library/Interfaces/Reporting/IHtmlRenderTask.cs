using System.Threading.Tasks;

namespace Nca.Library.Interfaces.Reporting
{
    public interface IHtmlRenderTask
    {
        Task<string> RenderAsync(IReportingModel model, string template);
    }
}
