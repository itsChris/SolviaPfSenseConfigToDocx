using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class InterfacesParser : IParser<List<Interface>>
    {
        public List<Interface> Parse(XElement interfacesElement)
        {
            HtmlDecodeTextOnly(interfacesElement);

            var interfaces = new List<Interface>();

            foreach (var ifaceElement in interfacesElement.Elements())
            {
                var iface = new Interface
                {
                    If = ifaceElement.Element("if")?.Value ?? string.Empty,
                    Enable = ifaceElement.Element("enable") != null,
                    Description = ifaceElement.Element("descr")?.Value ?? string.Empty,
                    IPAddr = ifaceElement.Element("ipaddr")?.Value ?? string.Empty,
                    Subnet = ifaceElement.Element("subnet")?.Value ?? string.Empty,
                    Gateway = ifaceElement.Element("gateway")?.Value ?? string.Empty,
                    BlockBogons = ifaceElement.Element("blockbogons")?.Value ?? string.Empty,
                    BlockPriv = ifaceElement.Element("blockprivate")?.Value ?? string.Empty,
                    SpoofMAC = ifaceElement.Element("spoofmac")?.Value ?? string.Empty
                };
                interfaces.Add(iface);
            }

            return interfaces;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }

}
