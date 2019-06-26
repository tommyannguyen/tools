using Nca.Library.Interfaces.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nca.Library.Models.Reporting
{
    public class ReportingSettings : IReportingSettings
    {
        public string OutPutDirectory { get; set; }

        public string TempPathDirectory { get; set; }

        public string HeaderTemplate { get; set; }

        public string FooterTemplate { get; set; }
    }
}
