using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
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
                    RefID = certElement.Element("refid")?.Value ?? string.Empty,
                    Description = certElement.Element("descr")?.Value ?? string.Empty,
                    Type = certElement.Element("type")?.Value ?? string.Empty,
                    Cert = certElement.Element("crt")?.Value ?? string.Empty,
                    PrivateKey = certElement.Element("prv")?.Value ?? string.Empty,
                    CARef = certElement.Element("caref")?.Value ?? string.Empty
                };

                certificateConfig.Certificates.Add(cert);
            }

            // Parse certificate authorities (CAs)
            foreach (var caElement in rootElement.Elements("ca"))
            {
                var ca = new CertificateAuthority
                {
                    RefID = caElement.Element("refid")?.Value ?? string.Empty,
                    Description = caElement.Element("descr")?.Value ?? string.Empty,
                    Trust = caElement.Element("trust")?.Value ?? string.Empty,
                    Cert = caElement.Element("crt")?.Value ?? string.Empty,
                    PrivateKey = caElement.Element("prv")?.Value ?? string.Empty
                };

                certificateConfig.CertificateAuthorities.Add(ca);
            }

            return certificateConfig;
        }
    }
}
