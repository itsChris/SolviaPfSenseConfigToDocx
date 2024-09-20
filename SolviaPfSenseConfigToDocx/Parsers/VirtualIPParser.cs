using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class VirtualIPParser : IParser<List<VirtualIP>>
    {
        public List<VirtualIP> Parse(XElement element)
        {
            var virtualIPs = new List<VirtualIP>();
            foreach (var vip in element.Elements("vip"))
            {
                var vi = new VirtualIP
                {
                    Mode = vip.Element("mode")?.Value ?? string.Empty,
                    Interface = vip.Element("interface")?.Value ?? string.Empty,
                    UniqId = vip.Element("uniqid")?.Value ?? string.Empty,
                    Description = vip.Element("descr")?.Value ?? string.Empty,
                    Type = vip.Element("type")?.Value ?? string.Empty,
                    SubnetBits = TryParseInt(vip.Element("subnet_bits")?.Value ?? string.Empty),
                    Subnet = vip.Element("subnet")?.Value ?? string.Empty,
                };
                virtualIPs.Add(vi);
            }
            return virtualIPs;
        }
        private int TryParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }
    }
}