using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    internal class OpenVPNServerConfigParser : IParser<OpenVPNServerConfig>
    {
        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }

        public OpenVPNServerConfig Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);

            var openvpnserverElement = element.Element("openvpn-server");

            if (openvpnserverElement == null)
                return null;

            var openVpnServerConfig = new OpenVPNServerConfig
            {
                VpnId = ParseNullableInt(openvpnserverElement.Element("vpnid")),
                Mode = openvpnserverElement.Element("mode")?.Value ?? string.Empty,
                AuthMode = openvpnserverElement.Element("authmode")?.Value ?? string.Empty,
                Protocol = openvpnserverElement.Element("protocol")?.Value ?? string.Empty,
                DevMode = openvpnserverElement.Element("dev_mode")?.Value ?? string.Empty,
                Interface = openvpnserverElement.Element("interface")?.Value ?? string.Empty,
                IpAddr = openvpnserverElement.Element("ipaddr")?.Value ?? string.Empty,
                LocalPort = ParseNullableInt(openvpnserverElement.Element("local_port")),
                Description = openvpnserverElement.Element("description")?.Value ?? string.Empty,
                CustomOptions = openvpnserverElement.Element("custom_options")?.Value ?? string.Empty,
                TLS = openvpnserverElement.Element("tls")?.Value ?? string.Empty,
                TLSType = openvpnserverElement.Element("tls_type")?.Value ?? string.Empty,
                TLSAuthKeyDir = openvpnserverElement.Element("tlsauth_keydir")?.Value ?? string.Empty,
                CaRef = openvpnserverElement.Element("caref")?.Value ?? string.Empty,
                CrlRef = openvpnserverElement.Element("crlref")?.Value ?? string.Empty,
                OcspUrl = openvpnserverElement.Element("ocspurl")?.Value ?? string.Empty,
                CertRef = openvpnserverElement.Element("certref")?.Value ?? string.Empty,
                DhLength = ParseNullableInt(openvpnserverElement.Element("dh_length")),
                EcdhCurve = openvpnserverElement.Element("ecdh_curve")?.Value ?? string.Empty,
                CertDepth = ParseNullableInt(openvpnserverElement.Element("cert_depth")),
                StrictUserCN = openvpnserverElement.Element("strictusercn")?.Value ?? string.Empty,
                RemoteCertTls = openvpnserverElement.Element("remote_cert_tls")?.Value ?? string.Empty,
                DataCiphersFallback = openvpnserverElement.Element("data_ciphers_fallback")?.Value ?? string.Empty,
                Digest = openvpnserverElement.Element("digest")?.Value ?? string.Empty,
                Engine = openvpnserverElement.Element("engine")?.Value ?? string.Empty,
                TunnelNetwork = openvpnserverElement.Element("tunnel_network")?.Value ?? string.Empty,
                TunnelNetworkV6 = openvpnserverElement.Element("tunnel_networkv6")?.Value ?? string.Empty,
                RemoteNetwork = openvpnserverElement.Element("remote_network")?.Value ?? string.Empty,
                RemoteNetworkV6 = openvpnserverElement.Element("remote_networkv6")?.Value ?? string.Empty,
                GWRedir = openvpnserverElement.Element("gwredir")?.Value ?? string.Empty,
                GWRedir6 = openvpnserverElement.Element("gwredir6")?.Value ?? string.Empty,
                LocalNetwork = openvpnserverElement.Element("local_network")?.Value ?? string.Empty,
                LocalNetworkV6 = openvpnserverElement.Element("local_networkv6")?.Value ?? string.Empty,
                MaxClients = ParseNullableInt(openvpnserverElement.Element("maxclients")),
                ConnLimit = ParseNullableInt(openvpnserverElement.Element("connlimit")),
                AllowCompression = openvpnserverElement.Element("allow_compression")?.Value ?? string.Empty,
                Compression = openvpnserverElement.Element("compression")?.Value ?? string.Empty,
                CompressionPush = openvpnserverElement.Element("compression_push")?.Value ?? string.Empty,
                PassTos = openvpnserverElement.Element("passtos")?.Value ?? string.Empty,
                Client2Client = openvpnserverElement.Element("client2client")?.Value ?? string.Empty,
                DynamicIP = openvpnserverElement.Element("dynamic_ip")?.Value ?? string.Empty,
                Topology = openvpnserverElement.Element("topology")?.Value ?? string.Empty,
                ServerBridgeDHCP = openvpnserverElement.Element("serverbridge_dhcp")?.Value ?? string.Empty,
                ServerBridgeInterface = openvpnserverElement.Element("serverbridge_interface")?.Value ?? string.Empty,
                ServerBridgeRouteGateway = openvpnserverElement.Element("serverbridge_routegateway")?.Value ?? string.Empty,
                ServerBridgeDHCPStart = openvpnserverElement.Element("serverbridge_dhcp_start")?.Value ?? string.Empty,
                ServerBridgeDHCPEnd = openvpnserverElement.Element("serverbridge_dhcp_end")?.Value ?? string.Empty,
                DnsDomain = openvpnserverElement.Element("dns_domain")?.Value ?? string.Empty,
                DnsServer1 = openvpnserverElement.Element("dns_server1")?.Value ?? string.Empty,
                DnsServer2 = openvpnserverElement.Element("dns_server2")?.Value ?? string.Empty,
                DnsServer3 = openvpnserverElement.Element("dns_server3")?.Value ?? string.Empty,
                DnsServer4 = openvpnserverElement.Element("dns_server4")?.Value ?? string.Empty,
                UsernameAsCommonName = openvpnserverElement.Element("username_as_common_name")?.Value ?? string.Empty,
                ExitNotify = ParseNullableInt(openvpnserverElement.Element("exit_notify")),
                SndRcvBuf = openvpnserverElement.Element("sndrcvbuf")?.Value ?? string.Empty,
                PushRegisterDns = openvpnserverElement.Element("push_register_dns")?.Value ?? string.Empty,
                NetbiosEnable = openvpnserverElement.Element("netbios_enable")?.Value ?? string.Empty,
                NetbiosNType = ParseNullableInt(openvpnserverElement.Element("netbios_ntype")),
                NetbiosScope = openvpnserverElement.Element("netbios_scope")?.Value ?? string.Empty,
                CreateGW = openvpnserverElement.Element("create_gw")?.Value ?? string.Empty,
                VerbosityLevel = ParseNullableInt(openvpnserverElement.Element("verbosity_level")),
                DataCiphers = openvpnserverElement.Element("data_ciphers")?.Value ?? string.Empty,
                PingMethod = openvpnserverElement.Element("ping_method")?.Value ?? string.Empty,
                KeepAliveInterval = ParseNullableInt(openvpnserverElement.Element("keepalive_interval")),
                KeepAliveTimeout = ParseNullableInt(openvpnserverElement.Element("keepalive_timeout")),
                PingSeconds = ParseNullableInt(openvpnserverElement.Element("ping_seconds")),
                PingPush = openvpnserverElement.Element("ping_push")?.Value ?? string.Empty,
                PingAction = openvpnserverElement.Element("ping_action")?.Value ?? string.Empty,
                PingActionSeconds = ParseNullableInt(openvpnserverElement.Element("ping_action_seconds")),
                PingActionPush = openvpnserverElement.Element("ping_action_push")?.Value ?? string.Empty,
                InactiveSeconds = ParseNullableInt(openvpnserverElement.Element("inactive_seconds"))
            };

            return openVpnServerConfig;
        }

        private int? ParseNullableInt(XElement element)
        {
            if (element == null || string.IsNullOrWhiteSpace(element.Value))
                return null;

            if (int.TryParse(element.Value, out int result))
                return result;

            return null;
        }
    }
}
