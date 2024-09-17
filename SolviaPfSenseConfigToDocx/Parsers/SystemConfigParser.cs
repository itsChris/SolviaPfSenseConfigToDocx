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
                NextGID = int.Parse(systemElement.Element("nextgid")?.Value ?? "0")
            };

            // Returning the parsed SystemConfig object
            return systemConfig;
        }
    }

}
