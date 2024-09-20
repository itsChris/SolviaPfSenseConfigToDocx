using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class PfSenseVersionParser : IParser<PfSense>
    {
        public PfSense Parse(System.Xml.Linq.XElement rootElement)
        {
            PfSense pfSense = new PfSense();
            var versionElement = rootElement.Element("version");
            pfSense.Version = versionElement?.Value;
            return pfSense;
        }
    }
}
