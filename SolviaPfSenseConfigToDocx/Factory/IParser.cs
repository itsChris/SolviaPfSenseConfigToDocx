using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Factory
{
    public interface IParser<T>
    {
        T Parse(XElement element);
        void HtmlDecodeTextOnly(XElement element);
    }
}
