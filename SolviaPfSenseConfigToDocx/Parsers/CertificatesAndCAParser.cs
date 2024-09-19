using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class CertificatesAndCAParser : IParser<CertificateConfig>
    {
        public CertificateConfig Parse(XElement rootElement)
        {
            var certificateConfig = new CertificateConfig
            {
                Certificates = new List<Certificate>(),
                CertificateAuthorities = new List<CertificateAuthority>()
            };

            // Parse certificates
            foreach (var certElement in rootElement.Elements("cert"))
            {
                var cert = new Certificate
                {
                    RefID = certElement.Element("refid")?.Value,
                    Description = certElement.Element("descr")?.Value,
                    Type = certElement.Element("type")?.Value,
                    Cert = certElement.Element("crt")?.Value,
                    PrivateKey = certElement.Element("prv")?.Value,
                    CARef = certElement.Element("caref")?.Value
                };

                certificateConfig.Certificates.Add(cert);
            }

            // Parse certificate authorities (CAs)
            foreach (var caElement in rootElement.Elements("ca"))
            {
                var ca = new CertificateAuthority
                {
                    RefID = caElement.Element("refid")?.Value,
                    Description = caElement.Element("descr")?.Value,
                    Trust = caElement.Element("trust")?.Value,
                    Cert = caElement.Element("crt")?.Value,
                    PrivateKey = caElement.Element("prv")?.Value
                };

                certificateConfig.CertificateAuthorities.Add(ca);
            }

            return certificateConfig;
        }
    }
}
