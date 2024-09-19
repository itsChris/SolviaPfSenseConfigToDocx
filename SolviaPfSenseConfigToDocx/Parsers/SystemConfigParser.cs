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
                Hostname = systemElement.Element("hostname")?.Value,
                Domain = systemElement.Element("domain")?.Value,
                NextUID = int.Parse(systemElement.Element("nextuid")?.Value ?? "0"),
                NextGID = int.Parse(systemElement.Element("nextgid")?.Value ?? "0"),
                Timeservers = systemElement.Elements("timeservers")?.Select(ts => ts.Value).ToList(),
                DisableSegmentationOffloading = systemElement.Element("disablesegmentationoffloading") != null,
                DisableLargeReceiveOffloading = systemElement.Element("disablelargereceiveoffloading") != null,
                IPv6Allow = systemElement.Element("ipv6allow") != null,
                MaximumTableEntries = int.Parse(systemElement.Element("maximumtableentries")?.Value ?? "0"),
                PowerSettings = new PowerSettings
                {
                    PowerdAcMode = systemElement.Element("powerd_ac_mode")?.Value,
                    PowerdBatteryMode = systemElement.Element("powerd_battery_mode")?.Value,
                    PowerdNormalMode = systemElement.Element("powerd_normal_mode")?.Value
                },
                SSH = new SSHConfig
                {
                    Enable = systemElement.Element("ssh")?.Element("enable")?.Value == "enabled"
                },
                DNSServers = systemElement.Elements("dnsserver")?.Select(dns => dns.Value).ToList(),
                DNSAllowOverride = systemElement.Element("dnsallowoverride") != null,
                DNS1GW = systemElement.Element("dns1gw")?.Value,
                DNS2GW = systemElement.Element("dns2gw")?.Value,
                SerialSpeed = int.Parse(systemElement.Element("serialspeed")?.Value ?? "0"),
                PrimaryConsole = systemElement.Element("primaryconsole")?.Value,
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
                Protocol = webGuiElement.Element("protocol")?.Value,
                SSLCertRef = webGuiElement.Element("ssl-certref")?.Value,
                DashboardColumns = int.Parse(webGuiElement.Element("dashboardcolumns")?.Value ?? "0"),
                WebGUICSS = webGuiElement.Element("webguicss")?.Value,
                LoginCSS = webGuiElement.Element("logincss")?.Value,
                MaxProcesses = int.Parse(webGuiElement.Element("max_procs")?.Value ?? "0"),
                Roaming = webGuiElement.Element("roaming")?.Value == "enabled",
                WebGUIFixedMenu = webGuiElement.Element("webguifixedmenu")?.Value == "fixed",
                WebGUIHostnameMenu = webGuiElement.Element("webguihostnamemenu")?.Value == "fqdn",
                LoginAutocomplete = webGuiElement.Element("loginautocomplete") != null,
                LoginShowHost = webGuiElement.Element("loginshowhost") != null
            };
        }
    }
}
