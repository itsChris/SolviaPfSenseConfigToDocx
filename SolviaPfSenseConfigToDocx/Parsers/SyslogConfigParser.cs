using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class SyslogConfigParser : IParser<SyslogConfig>
    {
        public SyslogConfig Parse(XElement syslogElement)
        {
            HtmlDecodeTextOnly(syslogElement);

            if (syslogElement == null)
            {
                throw new ArgumentNullException(nameof(syslogElement), "XElement cannot be null.");
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SyslogConfig));
                using (var reader = syslogElement.CreateReader())
                {
                    return (SyslogConfig)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error parsing syslog configuration.", ex);
            }
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}