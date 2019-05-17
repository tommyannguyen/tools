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
        private readonly BIConfiguration _configuration;

        public EmbedService(BIConfiguration biConfiguration)
        {
            this._configuration = biConfiguration;
        }

        public async Task<IEnumerable<ReportInfo>> GetReports()
        {
            using (var client = new PowerBIClient(MappingReportApiUrl(), GetTokenCredentials()))
            {
                var reports = await client.Reports.GetReportsInGroupAsync(_configuration.WorkspaceId);
                return reports.Value.Select(r => ConvertReportInfo(r));
            }
        }

        public async Task<EmbedReport> GetEmbedReport(ReportId reportId)
        {
            using (var client = new PowerBIClient(MappingReportApiUrl(reportId), GetTokenCredentials()))
            {
                var report = await client.Reports.GetReportAsync(reportId.Id);              
                return ConvertEmbedReport(report, await GenerateTotkenAsync(client, report));
            }
        }
        
        public Task<EmbedReport> GetEmbedReport(ReportGroup reportGroup)
        {
            return GetEmbedReport(reportGroup.ReportId);
        }

        private TokenCredentials GetTokenCredentials()
        {
            return null;
        }
        private async Task<EmbedToken> GenerateTotkenAsync(IPowerBIClient client ,Report report)
        {
            GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
            return await client.Reports.GenerateTokenInGroupAsync(_configuration.WorkspaceId, report.Id, generateTokenRequestParameters);
        }
        private EmbedReport ConvertEmbedReport(Report report, EmbedToken embedToken)
        {
            return new EmbedReport()
            {
                Id = new ReportId(report.Id),
                EmbedToken = embedToken,
                EmbedUrl = report.EmbedUrl
            };
        }
        private ReportInfo ConvertReportInfo(Report report)
        {
            return new ReportInfo(new ReportId(report.Id), MappingReportGroups(report));
        }
        
        //TODO :: Database mapping
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

        public Uri MappingReportApiUrl(ReportId reportId = null)
        {
            return new Uri(_configuration.ApiUrl);
        }
    }
}
