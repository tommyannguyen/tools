using Microsoft.Extensions.Logging;
using Nca.Library.Interfaces.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nca.Library.Repositories.Reporting
{
    internal class CleanupHtmlRenderTask : IHtmlRenderTask
    {
        private Dictionary<string, string> _linkedCssCache = new Dictionary<string, string>();
        private Dictionary<string, string> _linkedImagesCache = new Dictionary<string, string>();
        private readonly ILogger _logger;
        private readonly IReportContext _reportContext;

        public CleanupHtmlRenderTask(IReportContext reportContext)
        {
            _logger = reportContext.LoggerFactory.CreateLogger<CleanupHtmlRenderTask>();
            _reportContext = reportContext;
        }
        public async Task<string> RenderAsync(IReportingModel model, string htmlSource)
        {
            htmlSource = ResolveCSSLinks(htmlSource);
            htmlSource = ResolveJsLinks(htmlSource);
            htmlSource = ResolveImageTag(htmlSource);
            var result = PreMailer.Net.PreMailer.MoveCssInline(htmlSource);
            return await Task.FromResult(result.Html);
        }
        private string ResolveCSSLinks(string html)
        {
            var matches = Regex.Matches(html, @"<link\s*\s+href=""([^""]+)""\s*/>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                try
                {
                    string css = string.Empty;
                    string link = match.Groups[1].Value;
                    if (!_linkedCssCache.ContainsKey(link))
                    {
                        if (link.Contains("http"))
                        {
                            var webRequest = HttpWebRequest.Create(link);
                            var webResponse = webRequest.GetResponse();

                            using (Stream stream = webResponse.GetResponseStream())
                            {
                                StreamReader sr = new StreamReader(stream);

                                css = RemovePremailerNetBrokenSelectorModifiers(sr.ReadToEnd());
                            }
                        }
                        else if (!string.IsNullOrEmpty(_reportContext.TemplatePath))
                        {
                            var currentPath = Path.Combine(_reportContext.TemplatePath , link.Replace("../", "").Replace("/", "\\"));
                            css = RemovePremailerNetBrokenSelectorModifiers(File.ReadAllText(currentPath));
                        }

                        _linkedCssCache.Add(link, css);
                    }
                    else
                        css = _linkedCssCache[link];

                    html = html.Replace(match.Value, string.Format("<style type=\"text/css\">{0}</style>", css));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Css error : ", ex);
                }
            }
            return html;
        }
        private static string RemovePremailerNetBrokenSelectorModifiers(string css)
        {
            return Regex.Replace(css, @"[:]+-[\-a-z0-9]+", string.Empty, RegexOptions.IgnoreCase);
        }

        private string ResolveJsLinks(string html)
        {
            var matches = Regex.Matches(html, @"<script\s*\s+src=""([^""]+)""\s*></script>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                try
                {
                    string js = string.Empty;
                    string link = match.Groups[1].Value;
                    if (!_linkedCssCache.ContainsKey(link))
                    {
                        if (link.Contains("http"))
                        {
                            var webRequest = HttpWebRequest.Create(link);
                            var webResponse = webRequest.GetResponse();

                            using (Stream stream = webResponse.GetResponseStream())
                            {
                                StreamReader sr = new StreamReader(stream);

                                js = RemovePremailerNetBrokenSelectorModifiers(sr.ReadToEnd());
                            }
                        }
                        else if (!string.IsNullOrEmpty(_reportContext.TemplatePath))
                        {
                            var currentPath = _reportContext.TemplatePath + link.Replace("../", "").Replace("/", "\\");
                            js = RemovePremailerNetBrokenSelectorModifiers(File.ReadAllText(currentPath));

                        }

                        _linkedCssCache.Add(link, js);
                    }
                    else
                        js = _linkedCssCache[link];

                    html = html.Replace(match.Value, string.Format("<script>{0}</script>", js));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Css error : ", ex);
                }
            }
            return html;
        }

        private string ResolveImageTag(string html)
        {
            var matches = Regex.Matches(html, @"<img\s[^\>]*src\s*=\s*[""']([^/<]*/[^""']+)[^\>]*\s*/*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                try
                {
                    string base64 = string.Empty;
                    string link = match.Groups[1].Value;
                    var imgClass = "";
                    if (!_linkedImagesCache.ContainsKey(link))
                    {
                        imgClass = link.Replace("../", "_").Replace("/", "_");
                        if (link.Contains("http"))
                        {
                            var webRequest = HttpWebRequest.Create(link);
                            var webResponse = webRequest.GetResponse();

                            using (Stream stream = webResponse.GetResponseStream())
                            {
                                StreamReader reader = new StreamReader(stream);
                                using (var memstream = new MemoryStream())
                                {
                                    reader.BaseStream.CopyTo(memstream);
                                    base64 = Convert.ToBase64String(memstream.ToArray());
                                }
                            }
                        }
                        else if(!string.IsNullOrEmpty(_reportContext.TemplatePath))
                        {
                            var currentPath =  Path.Combine(_reportContext.TemplatePath , link.Replace("../", "").Replace("/", "\\"));
                            if (File.Exists(currentPath))
                            {
                                base64 = Convert.ToBase64String(File.ReadAllBytes(currentPath));
                            }
                        }

                        _linkedImagesCache.Add(link, base64);
                    }
                    else
                        base64 = _linkedCssCache[link];

                    html = html.Replace(match.Value, string.Format("<img src='data:image/png;base64,{0}' class='{1}'/>", base64, imgClass));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Css error : ", ex);
                }
            }
            return html;
        }
    }
}
