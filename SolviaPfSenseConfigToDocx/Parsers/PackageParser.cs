using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class PackageParser : IParser<List<Package>>
    {
        public List<Package> Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);

            var packages = new List<Package>();
            foreach (var item in element.Elements("package"))
            {
                var p = new Package
                {
                    Name = item.Element("name")?.Value ?? string.Empty,
                    Description = item.Element("descr")?.Value ?? string.Empty,
                    Website = item.Element("website")?.Value ?? string.Empty,
                    Version = item.Element("version")?.Value ?? string.Empty,
                    PkgInfoLink = item.Element("pkginfolink")?.Value ?? string.Empty,
                    ConfigurationFile = item.Element("configurationfile")?.Value ?? string.Empty,
                    IncludeFile = item.Element("includefile")?.Value ?? string.Empty
                };
                packages.Add(p);
            }
            return packages;
        }
        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}