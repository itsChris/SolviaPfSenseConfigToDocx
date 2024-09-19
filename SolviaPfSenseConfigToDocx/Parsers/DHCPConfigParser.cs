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
                    Enable = dhcpv4Element.Element("enable") != null,
                    FailoverPeerIP = dhcpv4Element.Element("failover_peerip")?.Value,
                    DefaultLeaseTime = int.TryParse(dhcpv4Element.Element("defaultleasetime")?.Value, out int defaultLeaseTime) ? defaultLeaseTime : 0,
                    MaxLeaseTime = int.TryParse(dhcpv4Element.Element("maxleasetime")?.Value, out int maxLeaseTime) ? maxLeaseTime : 0,
                    Netmask = dhcpv4Element.Element("netmask")?.Value,
                    TFTP = dhcpv4Element.Element("tftp")?.Value,
                    NextServer = dhcpv4Element.Element("nextserver")?.Value,
                    Filename = dhcpv4Element.Element("filename")?.Value,
                    RootPath = dhcpv4Element.Element("rootpath")?.Value,
                    NumberOptions = int.TryParse(dhcpv4Element.Element("numberoptions")?.Value, out int numberOptions) ? numberOptions : 0
                };

                // Parse DDNS Settings
                dhcpv4Config.DDNS = new DDNSSettings
                {
                    DDNSDomain = dhcpv4Element.Element("ddnsdomain")?.Value,
                    DDNSPrimary = dhcpv4Element.Element("ddnsdomainprimary")?.Value,
                    DDNSPrimaryPort = int.TryParse(dhcpv4Element.Element("ddnsdomainprimaryport")?.Value, out int ddnsPrimaryPort) ? ddnsPrimaryPort : 0,
                    DDNSSecondary = dhcpv4Element.Element("ddnsdomainsecondary")?.Value,
                    DDNSSecondaryPort = int.TryParse(dhcpv4Element.Element("ddnsdomainsecondaryport")?.Value, out int ddnsSecondaryPort) ? ddnsSecondaryPort : 0,
                    DDNSKeyName = dhcpv4Element.Element("ddnsdomainkeyname")?.Value,
                    DDNSKeyAlgorithm = dhcpv4Element.Element("ddnsdomainkeyalgorithm")?.Value,
                    DDNSKey = dhcpv4Element.Element("ddnsdomainkey")?.Value
                };

                // Parse Static Mappings
                foreach (var staticMapElement in dhcpv4Element.Elements("staticmap"))
                {
                    var staticMapping = new StaticMapping
                    {
                        MAC = staticMapElement.Element("mac")?.Value,
                        CID = staticMapElement.Element("cid")?.Value,
                        IPAddr = staticMapElement.Element("ipaddr")?.Value,
                        Hostname = staticMapElement.Element("hostname")?.Value,
                        Description = staticMapElement.Element("descr")?.Value,
                        Filename = staticMapElement.Element("filename")?.Value,
                        RootPath = staticMapElement.Element("rootpath")?.Value,
                        DefaultLeaseTime = int.TryParse(staticMapElement.Element("defaultleasetime")?.Value, out int defaultLeaseTime2) ? defaultLeaseTime2 : 0,
                        MaxLeaseTime = int.TryParse(staticMapElement.Element("maxleasetime")?.Value, out int maxLeaseTime2) ? maxLeaseTime2 : 0,
                        Gateway = staticMapElement.Element("gateway")?.Value,
                        Domain = staticMapElement.Element("domain")?.Value,
                        TFTP = staticMapElement.Element("tftp")?.Value,
                        NextServer = staticMapElement.Element("nextserver")?.Value
                    };

                    // Parse additional settings for the static mapping
                    staticMapping.DDNS = new DDNSSettings
                    {
                        DDNSDomain = staticMapElement.Element("ddnsdomain")?.Value,
                        DDNSPrimary = staticMapElement.Element("ddnsdomainprimary")?.Value,
                        DDNSPrimaryPort = int.Parse(staticMapElement.Element("ddnsdomainprimaryport")?.Value ?? "0"),
                        DDNSSecondary = staticMapElement.Element("ddnsdomainsecondary")?.Value,
                        DDNSSecondaryPort = int.Parse(staticMapElement.Element("ddnsdomainsecondaryport")?.Value ?? "0"),
                        DDNSKeyName = staticMapElement.Element("ddnsdomainkeyname")?.Value,
                        DDNSKeyAlgorithm = staticMapElement.Element("ddnsdomainkeyalgorithm")?.Value,
                        DDNSKey = staticMapElement.Element("ddnsdomainkey")?.Value
                    };

                    dhcpv4Config.StaticMappings.Add(staticMapping);
                }

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
