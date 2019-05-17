using Microsoft.PowerBI.Api.V2.Models;
using System;
using System.Collections.Generic;

namespace Nca.Library.Models
{
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
