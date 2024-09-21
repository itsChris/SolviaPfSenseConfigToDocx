using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class GatewayParser : IParser<List<Gateway>>
    {
        public List<Gateway> Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);

            var gateways = new List<Gateway>();
            foreach (var gw in element.Elements("gateway_item"))
            {
                var gateway = new Gateway
                {
                    Interface = gw.Element("interface")?.Value ?? string.Empty,
                    GatewayAddress = gw.Element("gateway")?.Value ?? string.Empty,
                    Name = gw.Element("name")?.Value ?? string.Empty,
                    Weight = TryParseInt(gw.Element("weight")?.Value ?? string.Empty),
                    IPProtocol = gw.Element("ipprotocol")?.Value ?? string.Empty,
                    Description = gw.Element("descr")?.Value ?? string.Empty,
                    GWDownKillStates = gw.Element("gw_down_kill_states") != null
                };
                gateways.Add(gateway);
            }
            return gateways;
        }
        private int TryParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }
        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}