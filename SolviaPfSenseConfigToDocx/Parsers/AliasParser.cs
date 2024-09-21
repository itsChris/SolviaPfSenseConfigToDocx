using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class AliasParser : IParser<List<Alias>>
    {
        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }

        public List<Alias> Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);
            var aliases = new List<Alias>();
            foreach (var alias in element.Elements("alias"))
            {
                var aliasItem = new Alias
                {
                    AliasName = alias.Element("name")?.Value ?? string.Empty,
                    Type = alias.Element("type")?.Value ?? string.Empty,
                    Address = alias.Elements("address").Select(a => a.Value).ToList(),
                    Description = alias.Element("descr")?.Value ?? string.Empty,
                    Detail = alias.Elements("detail").Select(d => d.Value).ToList()
                };
                aliases.Add(aliasItem);
            }
            return aliases;
        }
    }
}