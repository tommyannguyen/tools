namespace Nca.Library.Interfaces.Reporting
{
    public interface IReportingModel
    {
        string Header { get;}
        string Footer { get; }
        object Data { get; }
    }
}
