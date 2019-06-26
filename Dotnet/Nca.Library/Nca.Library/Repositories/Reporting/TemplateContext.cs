using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nca.Library.Repositories.Reporting
{
    public class ReportContext : IReportContext
    {
        public ReportContext(ILoggerFactory loggerFactory, string tempPath, string templatePath)
        {
            LoggerFactory = loggerFactory;
            TempPath = tempPath;
            TemplatePath = templatePath;
            CreateSessionId();
        }
        public string SessionId { get; private set; }
        public string TempPath { get; private set; }
        public string TemplatePath { get; }
        public ILoggerFactory LoggerFactory { get; private set; }

        private string GetSessionPath()
        {
            return $"{TempPath}\\{SessionId}";
        }
        private void CreateSessionId()
        {
            SessionId = Guid.NewGuid().ToString();
            Directory.CreateDirectory(GetSessionPath());
        }

        #region IDisposable Support
        private bool disposedValue = false;
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Clean up directory
                    Directory.Delete(GetSessionPath(), true);
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
