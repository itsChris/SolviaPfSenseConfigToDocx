using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class PfSenseConfigParser
    {
        private ParserFactory _parserFactory;

        public PfSenseConfigParser(string xmlFilePath)
        {
            _parserFactory = new ParserFactory(xmlFilePath);
        }

        public SystemConfig ParseSystemConfig()
        {
            var systemConfigParser = new SystemConfigParser();
            return _parserFactory.ParseSection("system", systemConfigParser);
        }

        public List<User> ParseUsers()
        {
            var userParser = new UserParser();
            return _parserFactory.ParseSection("system", userParser);
        }

        public List<Group> ParseGroups()
        {
            var groupParser = new GroupParser();
            return _parserFactory.ParseSection("system", groupParser);
        }

        public List<Interface> ParseInterfaces()
        {
            var interfacesParser = new InterfacesParser();
            return _parserFactory.ParseSection("interfaces", interfacesParser);
        }

        public DHCPConfig ParseDHCPConfig()
        {
            var dhcpConfigParser = new DHCPConfigParser();
            return _parserFactory.ParseSection("dhcpd", dhcpConfigParser);
        }

        public FirewallConfig ParseFirewallRulesAndNAT()
        {
            var firewallRulesAndNATParser = new FirewallRulesAndNATParser();
            return _parserFactory.ParseSection("filter", firewallRulesAndNATParser);
        }

        public IpSecVPNConfig ParseIpSecVPNConfig()
        {
            var ipSecVpnConfigParser = new IpSecVPNConfigParser();
            return _parserFactory.ParseSection("ipsec", ipSecVpnConfigParser);  // 'config' is the root element that contains 'ipsec' and 'openvpn'
        }

        public CertificateConfig ParseCertificatesAndCA()
        {
            var certificatesAndCAParser = new CertificatesAndCAParser();
            return _parserFactory.ParseSection("", certificatesAndCAParser); // Passing the root element
        }

        public List<StaticRoute> ParseStaticRoutes()
        {
            var staticRoutesParser = new StaticRoutesParser();
            return _parserFactory.ParseSection("staticroutes", staticRoutesParser);
        }

        public OtherConfigurations ParseOtherConfigurations()
        {
            var otherConfigurationParser = new OtherConfigurationParser();
            return _parserFactory.ParseSection("config", otherConfigurationParser);
        }

        public Dictionary<string, List<string>> ParseConfig()
        {
            var parsedData = new Dictionary<string, List<string>>();

            // Parse SystemConfig
            var systemConfig = ParseSystemConfig();
            parsedData["SystemConfig"] = new List<string>
                {
                    $"Hostname: {systemConfig.Hostname}",
                    $"Domain: {systemConfig.Domain}",
                    $"NextUID: {systemConfig.NextUID}",
                    $"NextGID: {systemConfig.NextGID}"
                };

            // Parse Users
            var users = ParseUsers();
            var userData = new List<string>();
            foreach (var user in users)
            {
                userData.Add($"User: {user.Name}, UID: {user.UID}");
            }
            parsedData["Users"] = userData;

            // Parse Groups
            var groups = ParseGroups();
            var groupData = new List<string>();
            foreach (var group in groups)
            {
                groupData.Add($"Group: {group.Name}, GID: {group.GID}");
            }
            parsedData["Groups"] = groupData;

            // Parse Interfaces
            var interfaces = ParseInterfaces();
            var interfacesData = new List<string>();
            foreach (var iface in interfaces)
            {
                interfacesData.Add($"Interface: {iface.If}, IP: {iface.IPAddr}, Description: {iface.Description}");
            }
            parsedData["Interfaces"] = interfacesData;

            // Parse DHCP Config
            var dhcpConfig = ParseDHCPConfig();
            parsedData["DHCP"] = new List<string>
                    {
                        $"DHCPv4 Range: {dhcpConfig.DHCPv4?.Range.From} - {dhcpConfig.DHCPv4?.Range.To}",
                        $"DHCPv6 Range: {dhcpConfig.DHCPv6?.Range.From} - {dhcpConfig.DHCPv6?.Range.To}"
                    };

            // Parse Firewall Rules and NAT
            var firewallConfig = ParseFirewallRulesAndNAT();
            var firewallData = new List<string>();
            foreach (var rule in firewallConfig.FirewallRules)
            {
                firewallData.Add($"Rule: {rule.Description}, Protocol: {rule.Protocol}");
            }
            parsedData["FirewallRules"] = firewallData;

            // Parse VPN Config
            var vpnConfig = ParseIpSecVPNConfig();
            var vpnData = new List<string>();

            // TODO: Add more details if needed
            if (vpnConfig != null)
            {
                foreach (var ipsec in vpnConfig.IPsecPhase1Configs)
                {
                    vpnData.Add($"IPSec Remote Gateway: {ipsec.RemoteGateway}");
                }
                parsedData["VPNConfigurations"] = vpnData;
            }

            // Parse Certificates and CA
            var certificatesAndCA = ParseCertificatesAndCA();
            var certData = new List<string>();

            // TODO: Add more details if needed
            if (certificatesAndCA != null)
            {
                foreach (var cert in certificatesAndCA.Certificates)
                {
                    certData.Add($"Certificate: {cert.Description}, RefID: {cert.RefID}");
                }
                parsedData["Certificates"] = certData;
            }

            // Parse Static Routes
            var staticRoutes = ParseStaticRoutes();
            var routeData = new List<string>();
            foreach (var route in staticRoutes)
            {
                routeData.Add($"Route: {route.Destination} -> {route.Gateway}");
            }
            parsedData["StaticRoutes"] = routeData;

            // Parse Other Configurations
            var otherConfigs = ParseOtherConfigurations();
            var aliasData = new List<string>();

            // TODO: Add more details if needed

            if(otherConfigs != null)
            {
                foreach (var alias in otherConfigs.Aliases)
                {
                    aliasData.Add($"Alias: {alias.AliasName}");
                }
                parsedData["OtherConfigurations"] = aliasData;

            }

            return parsedData;
        }
    }
}