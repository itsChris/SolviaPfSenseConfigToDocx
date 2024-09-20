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
                        From = dhcpv4Element.Element("range")?.Element("from")?.Value ?? string.Empty,
                        To = dhcpv4Element.Element("range")?.Element("to")?.Value ?? string.Empty
                    },
                    Gateway = dhcpv4Element.Element("gateway")?.Value ?? string.Empty,
                    Domain = dhcpv4Element.Element("domain")?.Value ?? string.Empty,
                    Enable = dhcpv4Element.Element("enable") != null,
                    FailoverPeerIP = dhcpv4Element.Element("failover_peerip")?.Value ?? string.Empty,
                    DefaultLeaseTime = int.TryParse(dhcpv4Element.Element("defaultleasetime")?.Value, out int defaultLeaseTime) ? defaultLeaseTime : 0,
                    MaxLeaseTime = int.TryParse(dhcpv4Element.Element("maxleasetime")?.Value, out int maxLeaseTime) ? maxLeaseTime : 0,
                    Netmask = dhcpv4Element.Element("netmask")?.Value ?? string.Empty,
                    TFTP = dhcpv4Element.Element("tftp")?.Value ?? string.Empty,
                    NextServer = dhcpv4Element.Element("nextserver")?.Value ?? string.Empty,
                    Filename = dhcpv4Element.Element("filename")?.Value ?? string.Empty,
                    RootPath = dhcpv4Element.Element("rootpath")?.Value ?? string.Empty,
                    NumberOptions = int.TryParse(dhcpv4Element.Element("numberoptions")?.Value, out int numberOptions) ? numberOptions : 0
                };

                // Parse DDNS Settings
                dhcpv4Config.DDNS = new DDNSSettings
                {
                    DDNSDomain = dhcpv4Element.Element("ddnsdomain")?.Value ?? string.Empty,
                    DDNSPrimary = dhcpv4Element.Element("ddnsdomainprimary")?.Value ?? string.Empty,
                    DDNSPrimaryPort = int.TryParse(dhcpv4Element.Element("ddnsdomainprimaryport")?.Value, out int ddnsPrimaryPort) ? ddnsPrimaryPort : 0,
                    DDNSSecondary = dhcpv4Element.Element("ddnsdomainsecondary")?.Value ?? string.Empty,
                    DDNSSecondaryPort = int.TryParse(dhcpv4Element.Element("ddnsdomainsecondaryport")?.Value, out int ddnsSecondaryPort) ? ddnsSecondaryPort : 0,
                    DDNSKeyName = dhcpv4Element.Element("ddnsdomainkeyname")?.Value ?? string.Empty,
                    DDNSKeyAlgorithm = dhcpv4Element.Element("ddnsdomainkeyalgorithm")?.Value ?? string.Empty,
                    DDNSKey = dhcpv4Element.Element("ddnsdomainkey")?.Value ?? string.Empty
                };

                // Parse Static Mappings
                foreach (var staticMapElement in dhcpv4Element.Elements("staticmap"))
                {
                    var staticMapping = new StaticMapping
                    {
                        MAC = staticMapElement.Element("mac")?.Value ?? string.Empty,
                        CID = staticMapElement.Element("cid")?.Value ?? string.Empty,
                        IPAddr = staticMapElement.Element("ipaddr")?.Value ?? string.Empty,
                        Hostname = staticMapElement.Element("hostname")?.Value ?? string.Empty,
                        Description = staticMapElement.Element("descr")?.Value ?? string.Empty,
                        Filename = staticMapElement.Element("filename")?.Value ?? string.Empty,
                        RootPath = staticMapElement.Element("rootpath")?.Value ?? string.Empty,
                        DefaultLeaseTime = int.TryParse(staticMapElement.Element("defaultleasetime")?.Value, out int defaultLeaseTime2) ? defaultLeaseTime2 : 0,
                        MaxLeaseTime = int.TryParse(staticMapElement.Element("maxleasetime")?.Value, out int maxLeaseTime2) ? maxLeaseTime2 : 0,
                        Gateway = staticMapElement.Element("gateway")?.Value ?? string.Empty,
                        Domain = staticMapElement.Element("domain")?.Value ?? string.Empty,
                        TFTP = staticMapElement.Element("tftp")?.Value ?? string.Empty,
                        NextServer = staticMapElement.Element("nextserver")?.Value ?? string.Empty
                    };

                    // Parse additional settings for the static mapping
                    staticMapping.DDNS = new DDNSSettings
                    {
                        DDNSDomain = staticMapElement.Element("ddnsdomain")?.Value ?? string.Empty,
                        DDNSPrimary = staticMapElement.Element("ddnsdomainprimary")?.Value ?? string.Empty,
                        DDNSPrimaryPort = int.TryParse(staticMapElement.Element("ddnsdomainprimaryport")?.Value, out int ddnsPrimaryPort2) ? ddnsPrimaryPort2 : 0,
                        DDNSSecondary = staticMapElement.Element("ddnsdomainsecondary")?.Value ?? string.Empty,
                        DDNSSecondaryPort = int.TryParse(staticMapElement.Element("ddnsdomainsecondaryport")?.Value, out int ddnsSecondaryPort2) ? ddnsSecondaryPort2 : 0,
                        DDNSKeyName = staticMapElement.Element("ddnsdomainkeyname")?.Value ?? string.Empty,
                        DDNSKeyAlgorithm = staticMapElement.Element("ddnsdomainkeyalgorithm")?.Value ?? string.Empty,
                        DDNSKey = staticMapElement.Element("ddnsdomainkey")?.Value ?? string.Empty
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
                        From = dhcpv6Element.Element("range")?.Element("from")?.Value ?? string.Empty,
                        To = dhcpv6Element.Element("range")?.Element("to")?.Value ?? string.Empty
                    },
                    RAMode = dhcpv6Element.Element("ramode")?.Value ?? string.Empty,
                    RAPriority = dhcpv6Element.Element("rapriority")?.Value ?? string.Empty
                };

                dhcpConfig.DHCPv6 = dhcpv6Config;
            }

            return dhcpConfig;
        }
    }

}
