using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Factory
{
    public class ParserFactory
    {
        private XDocument _xmlDocument;

        public ParserFactory(string xmlFilePath)
        {
            _xmlDocument = XDocument.Load(xmlFilePath);
        }

        public T ParseSection<T>(string sectionName, IParser<T> parser)
        {
            var element = _xmlDocument.Root.Element(sectionName);
            return element != null ? parser.Parse(element) : default;
        }
    }
}
