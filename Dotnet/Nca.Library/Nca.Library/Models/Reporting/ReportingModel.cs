using Nca.Library.Interfaces.Reporting;

namespace Nca.Library.Models.Reporting
{
    internal class ReportingModel : IReportingModel
    {
        public ReportingModel(string header, string footer, object data)
        {
            Header = header;
            Footer = footer;
            Data = data;
        }
        public string Header { get; private set; }

        public string Footer { get; private set; }
        public object Data { get; private set; }
    }
}
