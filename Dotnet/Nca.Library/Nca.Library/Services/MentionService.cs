using Nca.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nca.Library.Services
{
    public class MentionService : IMentionService
    {
        private const string pattern = @"<user.*id=\""(.*)\""\s*>.*</user>";
        public async Task<IEnumerable<string>> DetectAsync(string html)
        {
            var matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var result = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    result.Add(match.Groups[1].Value);
                }
            }

            return result;
        }
    }
}
