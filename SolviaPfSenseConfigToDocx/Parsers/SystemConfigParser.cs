using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class SystemConfigParser : IParser<SystemConfig>
    {
        public SystemConfig Parse(XElement systemElement)
        {
            var systemConfig = new SystemConfig
            {
                Hostname = systemElement.Element("hostname")?.Value ?? string.Empty,
                Domain = systemElement.Element("domain")?.Value ?? string.Empty,
                NextUID = TryParseInt(systemElement.Element("nextuid")?.Value ?? string.Empty),
                NextGID = TryParseInt(systemElement.Element("nextgid")?.Value ?? string.Empty),
                Timeservers = systemElement.Elements("timeservers")?.Select(ts => ts.Value ?? string.Empty).ToList(),
                DisableSegmentationOffloading = systemElement.Element("disablesegmentationoffloading") != null,
                DisableLargeReceiveOffloading = systemElement.Element("disablelargereceiveoffloading") != null,
                IPv6Allow = systemElement.Element("ipv6allow") != null,
                MaximumTableEntries = TryParseInt(systemElement.Element("maximumtableentries")?.Value ?? string.Empty),
                PowerSettings = new PowerSettings
                {
                    PowerdAcMode = systemElement.Element("powerd_ac_mode")?.Value ?? string.Empty,
                    PowerdBatteryMode = systemElement.Element("powerd_battery_mode")?.Value ?? string.Empty,
                    PowerdNormalMode = systemElement.Element("powerd_normal_mode")?.Value ?? string.Empty
                },
                SSH = new SSHConfig
                {
                    Enable = systemElement.Element("ssh")?.Element("enable")?.Value == "enabled"
                },
                DNSServers = systemElement.Elements("dnsserver")?.Select(dns => dns.Value ?? string.Empty).ToList(),
                DNSAllowOverride = systemElement.Element("dnsallowoverride") != null,
                DNS1GW = systemElement.Element("dns1gw")?.Value ?? string.Empty,
                DNS2GW = systemElement.Element("dns2gw")?.Value ?? string.Empty,
                SerialSpeed = TryParseInt(systemElement.Element("serialspeed")?.Value ?? string.Empty),
                PrimaryConsole = systemElement.Element("primaryconsole")?.Value ?? string.Empty,
                WebGUI = ParseWebGUIConfig(systemElement.Element("webgui"))
            };

            // Parse groups and users using other parsers
            var groupParser = new GroupParser();
            var userParser = new UserParser();
            systemConfig.Groups = groupParser.Parse(systemElement);
            systemConfig.Users = userParser.Parse(systemElement);

            return systemConfig;
        }

        private WebGUIConfig ParseWebGUIConfig(XElement webGuiElement)
        {
            if (webGuiElement == null) return null;

            return new WebGUIConfig
            {
                Protocol = webGuiElement.Element("protocol")?.Value ?? string.Empty,
                SSLCertRef = webGuiElement.Element("ssl-certref")?.Value ?? string.Empty,
                DashboardColumns = TryParseInt(webGuiElement.Element("dashboardcolumns")?.Value ?? string.Empty),
                WebGUICSS = webGuiElement.Element("webguicss")?.Value ?? string.Empty,
                LoginCSS = webGuiElement.Element("logincss")?.Value ?? string.Empty,
                MaxProcesses = TryParseInt(webGuiElement.Element("max_procs")?.Value ?? string.Empty),
                Roaming = webGuiElement.Element("roaming")?.Value == "enabled",
                WebGUIFixedMenu = webGuiElement.Element("webguifixedmenu")?.Value == "fixed",
                WebGUIHostnameMenu = webGuiElement.Element("webguihostnamemenu")?.Value == "fqdn",
                LoginAutocomplete = webGuiElement.Element("loginautocomplete") != null,
                LoginShowHost = webGuiElement.Element("loginshowhost") != null
            };
        }
        private int TryParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }
    }
}
