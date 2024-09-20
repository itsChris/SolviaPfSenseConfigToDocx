using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class IpSecConnectionParser : IParser<List<IpSecConnection>>
    {
        public List<IpSecConnection> Parse(XElement element)
        {
            // Parse Phase 1 entries
            var phase1Entries = element.Descendants("phase1").Select(p1 => new IPsecPhase1
            {
                    IKEID = TryParseInt(p1.Element("ikeid")?.Value),
                    IKEType = p1.Element("iketype")?.Value,
                    Interface = p1.Element("interface")?.Value,
                    RemoteGateway = p1.Element("remote-gateway")?.Value,
                    Protocol = p1.Element("protocol")?.Value,
                    MyIDType = p1.Element("myid_type")?.Value,
                    PeerIDType = p1.Element("peerid_type")?.Value,
                    Lifetime = TryParseInt(p1.Element("lifetime")?.Value),
                    RekeyTime = TryParseInt(p1.Element("rekey_time")?.Value),
                    RandTime = TryParseInt(p1.Element("rand_time")?.Value),
                    PreSharedKey = p1.Element("pre-shared-key")?.Value,
                    PrivateKey = p1.Element("private-key")?.Value,
                    CertRef = p1.Element("certref")?.Value,
                    PKCS11CertRef = p1.Element("pkcs11certref")?.Value,
                    PKCS11Pin = p1.Element("pkcs11pin")?.Value,
                    Caref = p1.Element("caref")?.Value,
                    AuthenticationMethod = p1.Element("authentication_method")?.Value,
                    Description = p1.Element("descr")?.Value,
                    NATTraversal = p1.Element("nat_traversal")?.Value == "on",
                    MOBIKE = p1.Element("mobike")?.Value == "off",
                    DPDDelay = TryParseInt(p1.Element("dpd_delay")?.Value),
                    DPDMaxFail = TryParseInt(p1.Element("dpd_maxfail")?.Value),
                    StartAction = p1.Element("startaction")?.Value,
                    CloseAction = p1.Element("closeaction")?.Value
            }).ToList();

            // Parse Phase 2 entries
            var phase2Entries = element.Descendants("phase2").Select(p2 => new IPsecPhase2
            {
                IKEID = TryParseInt(p2.Element("ikeid")?.Value),
                UniqID = p2.Element("uniqid")?.Value,
                Mode = p2.Element("mode")?.Value,
                ReqID = p2.Element("reqid")?.Value,
                LocalID = new VPNID
                {
                    Type = p2.Element("localid")?.Element("type")?.Value,
                    Address = p2.Element("localid")?.Element("address")?.Value,
                    Netbits = TryParseInt(p2.Element("localid")?.Element("netbits")?.Value)
                },
                RemoteID = new VPNID
                {
                    Type = p2.Element("remoteid")?.Element("type")?.Value,
                    Address = p2.Element("remoteid")?.Element("address")?.Value,
                    Netbits = TryParseInt(p2.Element("remoteid")?.Element("netbits")?.Value)
                },
                Lifetime = TryParseInt(p2.Element("lifetime")?.Value),
                RekeyTime = TryParseInt(p2.Element("rekey_time")?.Value),
                RandTime = TryParseInt(p2.Element("rand_time")?.Value),
                PingHost = p2.Element("pinghost")?.Value,
                Keepalive = p2.Element("keepalive")?.Value == "on",
                Description = p2.Element("descr")?.Value
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
    }
}