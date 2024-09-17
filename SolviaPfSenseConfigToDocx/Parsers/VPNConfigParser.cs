using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class VPNConfigParser : IParser<VPNConfig>
    {
        public VPNConfig Parse(XElement rootElement)
        {
            var vpnConfig = new VPNConfig
            {
                IPsecPhase1Configs = new List<IPsecPhase1>(),
                IPsecPhase2Configs = new List<IPsecPhase2>(),
                OpenVPNConfigs = new List<OpenVPNConfig>()
            };

            // Parse IPsec Phase 1 and 2 configurations
            var ipsecElement = rootElement.Element("ipsec");
            if (ipsecElement != null)
            {
                // Phase 1
                foreach (var phase1Element in ipsecElement.Elements("phase1"))
                {
                    var ipsecPhase1 = new IPsecPhase1
                    {
                        IKEID = int.Parse(phase1Element.Element("ikeid")?.Value ?? "0"),
                        RemoteGateway = phase1Element.Element("remotegw")?.Value,
                        MyIDType = phase1Element.Element("myid_type")?.Value,
                        PeerIDType = phase1Element.Element("peerid_type")?.Value
                    };
                    vpnConfig.IPsecPhase1Configs.Add(ipsecPhase1);
                }

                // Phase 2
                foreach (var phase2Element in ipsecElement.Elements("phase2"))
                {
                    var ipsecPhase2 = new IPsecPhase2
                    {
                        IKEID = int.Parse(phase2Element.Element("ikeid")?.Value ?? "0"),
                        LocalID = new VPNID
                        {
                            Address = phase2Element.Element("localid")?.Element("address")?.Value
                        },
                        RemoteID = new VPNID
                        {
                            Address = phase2Element.Element("remoteid")?.Element("address")?.Value
                        }
                    };
                    vpnConfig.IPsecPhase2Configs.Add(ipsecPhase2);
                }
            }

            // Parse OpenVPN configurations
            var openvpnElement = rootElement.Element("openvpn");
            if (openvpnElement != null)
            {
                foreach (var serverElement in openvpnElement.Elements("openvpn-server"))
                {
                    var openvpnConfig = new OpenVPNConfig
                    {
                        VPNID = int.Parse(serverElement.Element("vpnid")?.Value ?? "0"),
                        Mode = serverElement.Element("mode")?.Value,
                        Protocol = serverElement.Element("protocol")?.Value,
                        IPAddress = serverElement.Element("ipaddr")?.Value,
                        TunnelNetwork = serverElement.Element("tunnel_network")?.Value,
                        RemoteNetwork = serverElement.Element("remote_network")?.Value
                    };
                    vpnConfig.OpenVPNConfigs.Add(openvpnConfig);
                }
            }

            return vpnConfig;
        }
    }
}
