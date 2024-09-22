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

        internal List<Alias> ParseAliases()
        {
            var aliasParser = new AliasParser();
            return _parserFactory.ParseSection("aliases", aliasParser);
        }

        public SystemConfig ParseSystemConfig()
        {
            var systemConfigParser = new SystemConfigParser();
            return _parserFactory.ParseSection("system", systemConfigParser);
        }

        public List<CronJob> ParseCronJobs()
        {
            var cronJobParser = new CronJobParser();
            return _parserFactory.ParseSection("cron", cronJobParser);
        }

        public PfSense PfSense() 
        {
            var pfSenseParser = new PfSenseVersionParser();
            return _parserFactory.ParseSection("", pfSenseParser);
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

        internal List<Gateway> ParseGateways()
        {
            var gatewayParser = new GatewayParser();
            return _parserFactory.ParseSection("gateways", gatewayParser);
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
            return _parserFactory.ParseSection("ipsec", ipSecVpnConfigParser);  
        }

        public CertificateConfig ParseCertificatesAndCA()
        {
            var certificatesAndCAParser = new CertificatesAndCAParser();
            return _parserFactory.ParseSection("", certificatesAndCAParser); 
        }

        public List<StaticRoute> ParseStaticRoutes()
        {
            var staticRoutesParser = new StaticRoutesParser();
            return _parserFactory.ParseSection("staticroutes", staticRoutesParser);
        }

        internal List<Package> ParsePackages()
        {
            var packageParser = new PackageParser();
            return _parserFactory.ParseSection("installedpackages", packageParser);
        }

        internal List<Service> ParseServices()
        {
            var serviceParser = new ServiceParser();
            return _parserFactory.ParseSection("installedpackages", serviceParser);
        }

        internal List<VirtualIP> ParseVirtualIPs()
        {
            var virtualIPParser = new VirtualIPParser();
            return _parserFactory.ParseSection("virtualip", virtualIPParser);
        }

        public OtherConfigurations ParseOtherConfigurations()
        {
            var otherConfigurationParser = new OtherConfigurationParser();
            return _parserFactory.ParseSection("", otherConfigurationParser); // passing the root element
        }

        internal List<IpSecConnection> ParseIpSecConnections()
        {
            var ipSecConnectionParser = new IpSecConnectionParser();
            return _parserFactory.ParseSection("ipsec", ipSecConnectionParser);
        }

        internal SyslogConfig ParseSysLogConfig()
        {
            var syslogConfigParser = new SyslogConfigParser();
            return _parserFactory.ParseSection("syslog", syslogConfigParser);
        }

        internal OpenVPNServerConfig ParseOpenVPNServerConfig()
        {
            var openVPNServerConfigParser = new OpenVPNServerConfigParser();
            return _parserFactory.ParseSection("openvpn", openVPNServerConfigParser);
        }
    }
}