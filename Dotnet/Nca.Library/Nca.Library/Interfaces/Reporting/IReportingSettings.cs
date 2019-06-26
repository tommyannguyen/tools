namespace Nca.Library.Interfaces.Reporting
{
    public interface IReportingSettings
    {
        string OutPutDirectory { get; }
        string TempPathDirectory { get; }
        string HeaderTemplate { get; }
        string FooterTemplate { get; }
    }
}
