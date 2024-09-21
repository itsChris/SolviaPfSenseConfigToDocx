using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class GroupParser : IParser<List<Group>>
    {
        public List<Group> Parse(XElement systemElement)
        {
            HtmlDecodeTextOnly(systemElement);

            var groups = new List<Group>();
            foreach (var groupElement in systemElement.Elements("group"))
            {
                var group = new Group
                {
                    Name = groupElement.Element("name")?.Value,
                    GID = int.Parse(groupElement.Element("gid")?.Value ?? "0"),
                    Description = groupElement.Element("description")?.Value
                };
                groups.Add(group);
            }

            return groups;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }

}
