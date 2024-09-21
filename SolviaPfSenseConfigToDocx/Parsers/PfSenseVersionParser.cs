using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class PfSenseVersionParser : IParser<PfSense>
    {
        public PfSense Parse(XElement rootElement)
        {
            HtmlDecodeTextOnly(rootElement);

            PfSense pfSense = new PfSense();
            var versionElement = rootElement.Element("version");
            pfSense.Version = versionElement?.Value;
            return pfSense;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}
