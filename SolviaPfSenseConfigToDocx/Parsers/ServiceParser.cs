using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class ServiceParser : IParser<List<Service>>
    {
        public List<Service> Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);
            var services = new List<Service>();
            foreach (var s in element.Elements("service"))
            {
                var svc = new Service
                {
                    Name = s.Element("name")?.Value ?? string.Empty,
                    RCFile = s.Element("rcfile")?.Value ?? string.Empty,
                    CustomPhpServiceStatusCommand = s.Element("custom_php_service_status_command")?.Value ?? string.Empty,
                    Description = s.Element("description")?.Value ?? string.Empty
                };
                services.Add(svc);
            }
            return services;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}