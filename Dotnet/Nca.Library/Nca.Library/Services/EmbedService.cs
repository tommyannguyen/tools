using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Nca.Library.Interfaces;
using Nca.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Nca.Library.Services
{
    public class EmbedService : IEmbedService
    {
        private static readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
        private static readonly string WorkspaceId = ConfigurationManager.AppSettings["workspaceId"];
        private static readonly string AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];
        private static readonly string ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];
        private static readonly string ApplicationId = ConfigurationManager.AppSettings["applicationId"];

        private static readonly string ReportId = ConfigurationManager.AppSettings["reportId"];

        private static readonly string AuthenticationType = ConfigurationManager.AppSettings["AuthenticationType"];
        private static readonly NameValueCollection sectionConfig = ConfigurationManager.GetSection(AuthenticationType) as NameValueCollection;
        private static readonly string ApplicationSecret = sectionConfig["applicationSecret"];
        private static readonly string Tenant = sectionConfig["tenant"];
        private static readonly string Username = sectionConfig["pbiUsername"];
        private static readonly string Password = sectionConfig["pbiPassword"];

        public EmbedService()
        {
        }

        public async Task<IEnumerable<ReportInfo>> GetReports()
        {
            using (var client = new PowerBIClient(new Uri(ApiUrl), GetTokenCredentials()))
            {
                var reports = await client.Reports.GetReportsInGroupAsync(WorkspaceId);
                return reports.Value.Select(r => Convert(r));
            }
        }

        public Task<EmbedReport> GetEmbedReport(ReportId reportId)
        {
            throw new NotImplementedException();
        }
        private TokenCredentials GetTokenCredentials()
        {
            return null;
        }
        public Task<EmbedReport> GetEmbedReport(ReportGroup reportGroup)
        {
            throw new NotImplementedException();
        }

        private ReportInfo Convert(Report report)
        {
            return new ReportInfo(new ReportId(report.Id), MappingReportGroups(report));
        }
        private IEnumerable<ReportGroup> MappingReportGroups(Report report)
        {
            switch (report.Name)
            {
                case "":
                    {
                        return new List<ReportGroup>() {
                            new ReportGroup(new ReportId(report.Id), "",0),
                            new ReportGroup(new ReportId(report.Id), "",1),
                            new ReportGroup(new ReportId(report.Id),"",2),
                            new ReportGroup(new ReportId(report.Id),"",3),
                        };
                    }
            }
            return new List<ReportGroup>();
        }
    }
}
