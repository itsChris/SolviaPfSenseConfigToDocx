using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Factory
{
    public interface IParser<T>
    {
        T Parse(XElement element);
    }
}
