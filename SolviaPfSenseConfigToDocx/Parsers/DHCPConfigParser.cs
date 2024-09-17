using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class DHCPConfigParser : IParser<DHCPConfig>
    {
        public DHCPConfig Parse(XElement dhcpElement)
        {
            var dhcpConfig = new DHCPConfig();

            // Parse DHCPv4 settings
            foreach (var dhcpv4Element in dhcpElement.Elements("lan"))
            {
                var dhcpv4Config = new DHCPv4Config
                {
                    Range = new DHCPRange
                    {
                        From = dhcpv4Element.Element("range")?.Element("from")?.Value,
                        To = dhcpv4Element.Element("range")?.Element("to")?.Value
                    },
                    Gateway = dhcpv4Element.Element("gateway")?.Value,
                    Domain = dhcpv4Element.Element("domain")?.Value,
                    Enable = dhcpv4Element.Element("enable") != null
                };

                dhcpConfig.DHCPv4 = dhcpv4Config;
            }

            // Parse DHCPv6 settings
            foreach (var dhcpv6Element in dhcpElement.Elements("dhcpdv6"))
            {
                var dhcpv6Config = new DHCPv6Config
                {
                    Range = new DHCPRange
                    {
                        From = dhcpv6Element.Element("range")?.Element("from")?.Value,
                        To = dhcpv6Element.Element("range")?.Element("to")?.Value
                    },
                    RAMode = dhcpv6Element.Element("ramode")?.Value,
                    RAPriority = dhcpv6Element.Element("rapriority")?.Value
                    //Enable = dhcpv6Element.Element("enable") != null
                };

                dhcpConfig.DHCPv6 = dhcpv6Config;
            }

            return dhcpConfig;
        }
    }

}
