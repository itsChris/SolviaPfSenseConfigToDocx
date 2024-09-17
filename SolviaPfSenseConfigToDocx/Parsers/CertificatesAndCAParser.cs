using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class CertificatesAndCAParser : IParser<CertificateConfig>
    {
        public CertificateConfig Parse(XElement certElement)
        {
            var certificateConfig = new CertificateConfig();

            // Parse Certificates
            foreach (var certItem in certElement.Elements("cert"))
            {
                var certificate = new Certificate
                {
                    RefID = certItem.Element("refid")?.Value,
                    Description = certItem.Element("descr")?.Value,
                    Cert = certItem.Element("crt")?.Value,
                    PRV = certItem.Element("prv")?.Value,
                    CARef = certItem.Element("caref")?.Value
                };

                certificateConfig.Certificates.Add(certificate);
            }

            // Parse Certificate Authorities (CA)
            foreach (var caItem in certElement.Elements("ca"))
            {
                var ca = new CA
                {
                    RefID = caItem.Element("refid")?.Value,
                    Description = caItem.Element("descr")?.Value,
                    Trust = bool.Parse(caItem.Element("trust")?.Value ?? "false"),
                    CRT = caItem.Element("crt")?.Value,
                    PRV = caItem.Element("prv")?.Value,
                    RandomSerial = bool.Parse(caItem.Element("randomserial")?.Value ?? "false"),
                    Serial = int.Parse(caItem.Element("serial")?.Value ?? "0")
                };

                certificateConfig.CertificateAuthorities.Add(ca);
            }

            return certificateConfig;
        }
    }

}
