using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class OtherConfigurationParser : IParser<OtherConfigurations>
    {
        public OtherConfigurations Parse(XElement rootElement)
        {
            var otherConfigurations = new OtherConfigurations();

            // Parse Aliases
            foreach (var aliasElement in rootElement.Element("aliases")?.Elements("alias") ?? Enumerable.Empty<XElement>())
            {
                var alias = new Alias
                {
                    AliasName = aliasElement.Element("name")?.Value,
                    Type = aliasElement.Element("type")?.Value,
                    Description = aliasElement.Element("descr")?.Value,
                    Address = aliasElement.Elements("address").Select(a => a.Value).ToList(),
                    Detail = aliasElement.Elements("detail").Select(d => d.Value).ToList()
                };
                otherConfigurations.Aliases.Add(alias);
            }

            // Parse SNMP
            var snmpElement = rootElement.Element("snmp");
            if (snmpElement != null)
            {
                otherConfigurations.SNMP = new SNMPConfig
                {
                    SysLocation = snmpElement.Element("syslocation")?.Value,
                    SysContact = snmpElement.Element("syscontact")?.Value,
                    ROCommunity = snmpElement.Element("rocommunity")?.Value
                };
            }

            // Parse Gateways
            foreach (var gatewayElement in rootElement.Element("gateways")?.Elements("gateway") ?? Enumerable.Empty<XElement>())
            {
                var gateway = new Gateway
                {
                    Interface = gatewayElement.Element("interface")?.Value,
                    GatewayAddress = gatewayElement.Element("gateway")?.Value,
                    Name = gatewayElement.Element("name")?.Value,
                    Weight = int.Parse(gatewayElement.Element("weight")?.Value ?? "0"),
                    IPProtocol = gatewayElement.Element("ipprotocol")?.Value
                };
                otherConfigurations.Gateways.Add(gateway);
            }

            // Parse Captive Portal
            var captivePortalElement = rootElement.Element("captiveportal");
            if (captivePortalElement != null)
            {
                otherConfigurations.CaptivePortal = new CaptivePortalConfig
                {
                    Interface = captivePortalElement.Element("interface")?.Value,
                    MaxClients = int.Parse(captivePortalElement.Element("maxclients")?.Value ?? "0"),
                    IdleTimeout = int.Parse(captivePortalElement.Element("idletimeout")?.Value ?? "0"),
                    HardTimeout = int.Parse(captivePortalElement.Element("hardtimeout")?.Value ?? "0"),
                    AuthenticationMethod = captivePortalElement.Element("auth_method")?.Value,
                    Enable = captivePortalElement.Element("enable") != null
                };
            }

            // Parse DNSMasq
            var dnsmasqElement = rootElement.Element("dnsmasq");
            if (dnsmasqElement != null)
            {
                otherConfigurations.DNSMasq = new DNSMasqConfig
                {
                    Enable = dnsmasqElement.Element("enable") != null,
                    DNSSEC = dnsmasqElement.Element("dnssec") != null,
                    DomainOverride = dnsmasqElement.Elements("domainoverride").Select(d => d.Value).ToList()
                };
            }

            // Parse Unbound DNS Resolver
            var unboundElement = rootElement.Element("unbound");
            if (unboundElement != null)
            {
                otherConfigurations.Unbound = new UnboundConfig
                {
                    Enable = unboundElement.Element("enable") != null,
                    DNSSEC = unboundElement.Element("dnssec") != null,
                    HideIdentity = unboundElement.Element("hideidentity") != null,
                    HideVersion = unboundElement.Element("hideversion") != null,
                    DomainOverride = unboundElement.Elements("domainoverride").Select(d => d.Value).ToList(),
                    CustomOptions = unboundElement.Element("custom_options")?.Value
                };
            }

            // Parse Cron Jobs
            foreach (var cronElement in rootElement.Element("cron")?.Elements("job") ?? Enumerable.Empty<XElement>())
            {
                var cronJob = new CronJob
                {
                    Minute = cronElement.Element("minute")?.Value,
                    Hour = cronElement.Element("hour")?.Value,
                    Command = cronElement.Element("command")?.Value
                };
                otherConfigurations.CronJobs.Add(cronJob);
            }

            // Parse Packages
            foreach (var packageElement in rootElement.Element("installedpackages")?.Elements("package") ?? Enumerable.Empty<XElement>())
            {
                var package = new Package
                {
                    Name = packageElement.Element("name")?.Value,
                    Version = packageElement.Element("version")?.Value,
                    Description = packageElement.Element("descr")?.Value,
                    Website = packageElement.Element("website")?.Value,
                    Logging = new Logging
                    {
                        LogFileName = packageElement.Element("logging")?.Element("logfilename")?.Value
                    }
                };
                otherConfigurations.Packages.Add(package);
            }

            // Parse Virtual IPs
            foreach (var vipElement in rootElement.Element("virtualip")?.Elements("vip") ?? Enumerable.Empty<XElement>())
            {
                var virtualIP = new VirtualIP
                {
                    Mode = vipElement.Element("mode")?.Value,
                    Interface = vipElement.Element("interface")?.Value,
                    Address = vipElement.Element("subnet")?.Value,
                    Description = vipElement.Element("descr")?.Value
                };
                otherConfigurations.VirtualIPs.Add(virtualIP);
            }

            return otherConfigurations;
        }
    }

}
