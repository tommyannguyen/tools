using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Nca.Library.Interfaces.Reporting
{
    public interface IReportContext : IDisposable
    {
        string SessionId { get; }
        string TempPath { get;}
        ILoggerFactory LoggerFactory { get; }
        string TemplatePath { get; }
    }
}
