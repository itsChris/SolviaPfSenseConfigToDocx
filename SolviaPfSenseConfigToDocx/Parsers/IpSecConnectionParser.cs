using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class IpSecConnectionParser : IParser<List<IpSecConnection>>
    {
        public List<IpSecConnection> Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);

            // Parse Phase 1 entries
            var phase1Entries = element.Descendants("phase1").Select(p1 => new IPsecPhase1
            {
                IKEID = TryParseInt(p1.Element("ikeid")?.Value ?? "0"),
                IKEType = p1.Element("iketype")?.Value ?? string.Empty,
                Interface = p1.Element("interface")?.Value ?? string.Empty,
                RemoteGateway = p1.Element("remote-gateway")?.Value ?? string.Empty,
                Protocol = p1.Element("protocol")?.Value ?? string.Empty,
                MyIDType = p1.Element("myid_type")?.Value ?? string.Empty,
                PeerIDType = p1.Element("peerid_type")?.Value ?? string.Empty,
                Lifetime = TryParseInt(p1.Element("lifetime")?.Value ?? "0"),
                RekeyTime = TryParseInt(p1.Element("rekey_time")?.Value ?? "0"),
                RandTime = TryParseInt(p1.Element("rand_time")?.Value ?? "0"),
                PreSharedKey = p1.Element("pre-shared-key")?.Value ?? string.Empty,
                PrivateKey = p1.Element("private-key")?.Value ?? string.Empty,
                CertRef = p1.Element("certref")?.Value ?? string.Empty,
                PKCS11CertRef = p1.Element("pkcs11certref")?.Value ?? string.Empty,
                PKCS11Pin = p1.Element("pkcs11pin")?.Value ?? string.Empty,
                Caref = p1.Element("caref")?.Value ?? string.Empty,
                AuthenticationMethod = p1.Element("authentication_method")?.Value ?? string.Empty,
                Description = p1.Element("descr")?.Value ?? string.Empty,
                NATTraversal = p1.Element("nat_traversal")?.Value == "on",
                MOBIKE = p1.Element("mobike")?.Value == "off",
                DPDDelay = TryParseInt(p1.Element("dpd_delay")?.Value ?? "0"),
                DPDMaxFail = TryParseInt(p1.Element("dpd_maxfail")?.Value ?? "0"),
                StartAction = p1.Element("startaction")?.Value ?? string.Empty,
                CloseAction = p1.Element("closeaction")?.Value ?? string.Empty
            }).ToList();

            // Parse Phase 2 entries
            var phase2Entries = element.Descendants("phase2").Select(p2 => new IPsecPhase2
            {
                IKEID = TryParseInt(p2.Element("ikeid")?.Value ?? "0"),
                UniqID = p2.Element("uniqid")?.Value ?? string.Empty,
                Mode = p2.Element("mode")?.Value ?? string.Empty,
                ReqID = p2.Element("reqid")?.Value ?? string.Empty,
                LocalID = new VPNID
                {
                    Type = p2.Element("localid")?.Element("type")?.Value ?? string.Empty,
                    Address = p2.Element("localid")?.Element("address")?.Value ?? string.Empty,
                    Netbits = TryParseInt(p2.Element("localid")?.Element("netbits")?.Value ?? "0")
                },
                RemoteID = new VPNID
                {
                    Type = p2.Element("remoteid")?.Element("type")?.Value ?? string.Empty,
                    Address = p2.Element("remoteid")?.Element("address")?.Value ?? string.Empty,
                    Netbits = TryParseInt(p2.Element("remoteid")?.Element("netbits")?.Value ?? "0")
                },
                Lifetime = TryParseInt(p2.Element("lifetime")?.Value ?? "0"),
                RekeyTime = TryParseInt(p2.Element("rekey_time")?.Value ?? "0"),
                RandTime = TryParseInt(p2.Element("rand_time")?.Value ?? "0"),
                PingHost = p2.Element("pinghost")?.Value ?? string.Empty,
                Keepalive = p2.Element("keepalive")?.Value == "on",
                Description = p2.Element("descr")?.Value ?? string.Empty
            }).ToList();

            // Match Phase 2 with Phase 1 based on IkeId
            var ipSecConnections = phase1Entries.Select(p1 => new IpSecConnection
            {
                Phase1 = p1,
                Phase2List = phase2Entries.Where(p2 => p2.IKEID == p1.IKEID).ToList()
            }).ToList();

            return ipSecConnections;
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