using System.Net;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.ExtensionMethods
{
    public static class XElementExtensions
    {
        public static void HtmlDecodeTextOnly(this XElement element)
        {
            // Decode the value of the current element if it is a leaf node (i.e., no children)
            if (!element.HasElements && !string.IsNullOrEmpty(element.Value))
            {
                element.Value = WebUtility.HtmlDecode(element.Value);
            }

            // Decode attributes of the current element
            foreach (var attribute in element.Attributes())
            {
                if (!string.IsNullOrEmpty(attribute.Value))
                {
                    attribute.Value = WebUtility.HtmlDecode(attribute.Value);
                }
            }

            // Recursively decode child elements
            foreach (var childElement in element.Elements())
            {
                childElement.HtmlDecodeTextOnly(); // Recursion
            }
        }
    }
}
