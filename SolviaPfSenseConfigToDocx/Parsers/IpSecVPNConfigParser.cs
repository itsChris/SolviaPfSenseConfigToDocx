using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class IpSecVPNConfigParser : IParser<IpSecVPNConfig>
    {
        public IpSecVPNConfig Parse(XElement rootElement)
        {
            HtmlDecodeTextOnly(rootElement);

            var vpnConfig = new IpSecVPNConfig
            {
                IPsecPhase1Configs = new List<IPsecPhase1>(),
                IPsecPhase2Configs = new List<IPsecPhase2>()
            };

            // Parse IPsec Phase 1 configurations
            foreach (var phase1Element in rootElement.Elements("phase1"))
            {
                var ipsecPhase1 = new IPsecPhase1
                {
                    IKEID = TryParseInt(phase1Element.Element("ikeid")?.Value),
                    IKEType = phase1Element.Element("iketype")?.Value,
                    Interface = phase1Element.Element("interface")?.Value,
                    RemoteGateway = phase1Element.Element("remote-gateway")?.Value,
                    Protocol = phase1Element.Element("protocol")?.Value,
                    MyIDType = phase1Element.Element("myid_type")?.Value,
                    PeerIDType = phase1Element.Element("peerid_type")?.Value,
                    Lifetime = TryParseInt(phase1Element.Element("lifetime")?.Value),
                    RekeyTime = TryParseInt(phase1Element.Element("rekey_time")?.Value),
                    RandTime = TryParseInt(phase1Element.Element("rand_time")?.Value),
                    PreSharedKey = phase1Element.Element("pre-shared-key")?.Value,
                    PrivateKey = phase1Element.Element("private-key")?.Value,
                    CertRef = phase1Element.Element("certref")?.Value,
                    PKCS11CertRef = phase1Element.Element("pkcs11certref")?.Value,
                    PKCS11Pin = phase1Element.Element("pkcs11pin")?.Value,
                    Caref = phase1Element.Element("caref")?.Value,
                    AuthenticationMethod = phase1Element.Element("authentication_method")?.Value,
                    Description = phase1Element.Element("descr")?.Value,
                    NATTraversal = phase1Element.Element("nat_traversal")?.Value == "on",
                    MOBIKE = phase1Element.Element("mobike")?.Value == "off",
                    DPDDelay = TryParseInt(phase1Element.Element("dpd_delay")?.Value),
                    DPDMaxFail = TryParseInt(phase1Element.Element("dpd_maxfail")?.Value),
                    StartAction = phase1Element.Element("startaction")?.Value,
                    CloseAction = phase1Element.Element("closeaction")?.Value
                };

                // Parse encryption algorithms
                foreach (var encryptionItem in phase1Element.Element("encryption")?.Elements("item") ?? new List<XElement>())
                {
                    var encryptionAlgorithm = new EncryptionAlgorithm
                    {
                        Name = encryptionItem.Element("encryption-algorithm")?.Element("name")?.Value,
                        KeyLength = TryParseInt(encryptionItem.Element("encryption-algorithm")?.Element("keylen")?.Value)
                    };
                    ipsecPhase1.EncryptionAlgorithms.Add(encryptionAlgorithm);

                    ipsecPhase1.HashAlgorithm = encryptionItem.Element("hash-algorithm")?.Value;
                    ipsecPhase1.PRFAlgorithm = encryptionItem.Element("prf-algorithm")?.Value;
                    ipsecPhase1.DHGroup = encryptionItem.Element("dhgroup")?.Value;
                }

                vpnConfig.IPsecPhase1Configs.Add(ipsecPhase1);
            }

            // Parse IPsec Phase 2 configurations
            foreach (var phase2Element in rootElement.Elements("phase2"))
            {
                var ipsecPhase2 = new IPsecPhase2
                {
                    IKEID = TryParseInt(phase2Element.Element("ikeid")?.Value),
                    UniqID = phase2Element.Element("uniqid")?.Value,
                    Mode = phase2Element.Element("mode")?.Value,
                    ReqID = phase2Element.Element("reqid")?.Value,
                    LocalID = new VPNID
                    {
                        Type = phase2Element.Element("localid")?.Element("type")?.Value,
                        Address = phase2Element.Element("localid")?.Element("address")?.Value,
                        Netbits = TryParseInt(phase2Element.Element("localid")?.Element("netbits")?.Value)
                    },
                    RemoteID = new VPNID
                    {
                        Type = phase2Element.Element("remoteid")?.Element("type")?.Value,
                        Address = phase2Element.Element("remoteid")?.Element("address")?.Value,
                        Netbits = TryParseInt(phase2Element.Element("remoteid")?.Element("netbits")?.Value)
                    },
                    Lifetime = TryParseInt(phase2Element.Element("lifetime")?.Value),
                    RekeyTime = TryParseInt(phase2Element.Element("rekey_time")?.Value),
                    RandTime = TryParseInt(phase2Element.Element("rand_time")?.Value),
                    PingHost = phase2Element.Element("pinghost")?.Value,
                    Keepalive = phase2Element.Element("keepalive")?.Value == "on",
                    Description = phase2Element.Element("descr")?.Value
                };

                // Parse encryption algorithm options
                foreach (var encryptionOption in phase2Element.Element("encryption")?.Elements("item") ?? new List<XElement>())
                {
                    var encryptionAlgorithmOption = new EncryptionAlgorithmOption
                    {
                        Name = encryptionOption.Element("encryption-algorithm")?.Element("name")?.Value,
                        KeyLength = TryParseInt(encryptionOption.Element("encryption-algorithm")?.Element("keylen")?.Value)
                    };
                    ipsecPhase2.EncryptionAlgorithmOptions.Add(encryptionAlgorithmOption);
                }

                ipsecPhase2.HashAlgorithmOption = phase2Element.Element("hash-algorithm")?.Value;
                ipsecPhase2.PFSGroup = phase2Element.Element("pfsgroup")?.Value;

                vpnConfig.IPsecPhase2Configs.Add(ipsecPhase2);
            }

            return vpnConfig;
        }

        private int TryParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}
