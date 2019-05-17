using Microsoft.PowerBI.Api.V2.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Nca.Library.Models
{
    public class BIConfiguration
    {
        public readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];//https://api.powerbi.com
        public readonly string WorkspaceId = ConfigurationManager.AppSettings["workspaceId"];
        public readonly string AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];
        public readonly string ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];
        public readonly string ApplicationId = ConfigurationManager.AppSettings["applicationId"];

        public readonly string ReportId = ConfigurationManager.AppSettings["reportId"];

        public readonly string AuthenticationType = ConfigurationManager.AppSettings["AuthenticationType"];
        public readonly NameValueCollection sectionConfig = ConfigurationManager.GetSection(AuthenticationType) as NameValueCollection;
        public readonly string ApplicationSecret = sectionConfig["applicationSecret"];
        public readonly string Tenant = sectionConfig["tenant"];
        public readonly string Username = sectionConfig["pbiUsername"];
        public readonly string Password = sectionConfig["pbiPassword"];
    }
    public class EmbedReport
    {
        public ReportId Id { get; set; }

        public string EmbedUrl { get; set; }

        public EmbedToken EmbedToken { get; set; }

        public int MinutesToExpiration
        {
            get
            {
                var minutesToExpiration = EmbedToken.Expiration.Value - DateTime.UtcNow;
                return (int)minutesToExpiration.TotalMinutes;
            }
        }
    }

    public class ReportInfo
    {
        public ReportInfo(ReportId id, IEnumerable<ReportGroup> groups)
        {
            Id = id;
            Groups = groups;
        }
        public ReportId Id { get; }
        public IEnumerable<ReportGroup> Groups { get; }
    }
    public class ReportId
    {
        public ReportId(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }
    public class ReportGroup
    {
        public ReportGroup(ReportId reportId, string name,int order)
        {
            ReportId = reportId;
            Name = name;
            Order = order;
        }
        public ReportId ReportId { get; }
        public string Name { get; }
        public int Order { get; }
    }
}
