using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class InterfacesParser : IParser<List<Interface>>
    {
        public List<Interface> Parse(XElement interfacesElement)
        {
            var interfaces = new List<Interface>();

            foreach (var ifaceElement in interfacesElement.Elements())
            {
                var iface = new Interface
                {
                    If = ifaceElement.Element("if")?.Value,
                    Enable = ifaceElement.Element("enable") != null,
                    Description = ifaceElement.Element("descr")?.Value,
                    IPAddr = ifaceElement.Element("ipaddr")?.Value,
                    Subnet = ifaceElement.Element("subnet")?.Value,
                    Gateway = ifaceElement.Element("gateway")?.Value
                };
                interfaces.Add(iface);
            }

            return interfaces;
        }
    }

}
