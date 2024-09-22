using SolviaPfSenseConfigToDocx.CustomAttributes;
using System.Xml.Serialization;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    [Serializable]
    [XmlRoot("openvpn-server")]
    public class OpenVPNServerConfig
    {
        [XmlElement("vpnid", IsNullable = true)]
        public int? VpnId { get; set; }

        [XmlElement("mode", IsNullable = true)]
        public string Mode { get; set; }

        [XmlElement("authmode", IsNullable = true)]
        public string AuthMode { get; set; }

        [XmlElement("protocol", IsNullable = true)]
        public string Protocol { get; set; }

        [XmlElement("dev_mode", IsNullable = true)]
        public string DevMode { get; set; }

        [XmlElement("interface", IsNullable = true)]
        public string Interface { get; set; }

        [XmlElement("ipaddr", IsNullable = true)]
        public string IpAddr { get; set; }

        [XmlElement("local_port", IsNullable = true)]
        public int? LocalPort { get; set; }  // Nullable int

        [XmlElement("description", IsNullable = true)]
        public string Description { get; set; }

        [XmlElement("custom_options", IsNullable = true)]
        public string CustomOptions { get; set; }

        [XmlElement("tls", IsNullable = true)]
        [Secret(true)]
        public string TLS { get; set; }

        [XmlElement("tls_type", IsNullable = true)]
        public string TLSType { get; set; }

        [XmlElement("tlsauth_keydir", IsNullable = true)]
        public string TLSAuthKeyDir { get; set; }

        [XmlElement("caref", IsNullable = true)]
        public string CaRef { get; set; }

        [XmlElement("crlref", IsNullable = true)]
        public string CrlRef { get; set; }

        [XmlElement("ocspurl", IsNullable = true)]
        public string OcspUrl { get; set; }

        [XmlElement("certref", IsNullable = true)]
        public string CertRef { get; set; }

        [XmlElement("dh_length", IsNullable = true)]
        public int? DhLength { get; set; }  // Nullable int

        [XmlElement("ecdh_curve", IsNullable = true)]
        public string EcdhCurve { get; set; }

        [XmlElement("cert_depth", IsNullable = true)]
        public int? CertDepth { get; set; }  // Nullable int

        [XmlElement("strictusercn", IsNullable = true)]
        public string StrictUserCN { get; set; }

        [XmlElement("remote_cert_tls", IsNullable = true)]
        public string RemoteCertTls { get; set; }

        [XmlElement("data_ciphers_fallback", IsNullable = true)]
        public string DataCiphersFallback { get; set; }

        [XmlElement("digest", IsNullable = true)]
        public string Digest { get; set; }

        [XmlElement("engine", IsNullable = true)]
        public string Engine { get; set; }

        [XmlElement("tunnel_network", IsNullable = true)]
        public string TunnelNetwork { get; set; }

        [XmlElement("tunnel_networkv6", IsNullable = true)]
        public string TunnelNetworkV6 { get; set; }

        [XmlElement("remote_network", IsNullable = true)]
        public string RemoteNetwork { get; set; }

        [XmlElement("remote_networkv6", IsNullable = true)]
        public string RemoteNetworkV6 { get; set; }

        [XmlElement("gwredir", IsNullable = true)]
        public string GWRedir { get; set; }

        [XmlElement("gwredir6", IsNullable = true)]
        public string GWRedir6 { get; set; }

        [XmlElement("local_network", IsNullable = true)]
        public string LocalNetwork { get; set; }

        [XmlElement("local_networkv6", IsNullable = true)]
        public string LocalNetworkV6 { get; set; }

        [XmlElement("maxclients", IsNullable = true)]
        public int? MaxClients { get; set; }  // Nullable int

        [XmlElement("connlimit", IsNullable = true)]
        public int? ConnLimit { get; set; }  // Nullable int

        [XmlElement("allow_compression", IsNullable = true)]
        public string AllowCompression { get; set; }

        [XmlElement("compression", IsNullable = true)]
        public string Compression { get; set; }

        [XmlElement("compression_push", IsNullable = true)]
        public string CompressionPush { get; set; }

        [XmlElement("passtos", IsNullable = true)]
        public string PassTos { get; set; }

        [XmlElement("client2client", IsNullable = true)]
        public string Client2Client { get; set; }

        [XmlElement("dynamic_ip", IsNullable = true)]
        public string DynamicIP { get; set; }

        [XmlElement("topology", IsNullable = true)]
        public string Topology { get; set; }

        [XmlElement("serverbridge_dhcp", IsNullable = true)]
        public string ServerBridgeDHCP { get; set; }

        [XmlElement("serverbridge_interface", IsNullable = true)]
        public string ServerBridgeInterface { get; set; }

        [XmlElement("serverbridge_routegateway", IsNullable = true)]
        public string ServerBridgeRouteGateway { get; set; }

        [XmlElement("serverbridge_dhcp_start", IsNullable = true)]
        public string ServerBridgeDHCPStart { get; set; }

        [XmlElement("serverbridge_dhcp_end", IsNullable = true)]
        public string ServerBridgeDHCPEnd { get; set; }

        [XmlElement("dns_domain", IsNullable = true)]
        public string DnsDomain { get; set; }

        [XmlElement("dns_server1", IsNullable = true)]
        public string DnsServer1 { get; set; }

        [XmlElement("dns_server2", IsNullable = true)]
        public string DnsServer2 { get; set; }

        [XmlElement("dns_server3", IsNullable = true)]
        public string DnsServer3 { get; set; }

        [XmlElement("dns_server4", IsNullable = true)]
        public string DnsServer4 { get; set; }

        [XmlElement("username_as_common_name", IsNullable = true)]
        public string UsernameAsCommonName { get; set; }

        [XmlElement("exit_notify", IsNullable = true)]
        public int? ExitNotify { get; set; }  // Nullable int

        [XmlElement("sndrcvbuf", IsNullable = true)]
        public string SndRcvBuf { get; set; }

        [XmlElement("push_register_dns", IsNullable = true)]
        public string PushRegisterDns { get; set; }

        [XmlElement("netbios_enable", IsNullable = true)]
        public string NetbiosEnable { get; set; }

        [XmlElement("netbios_ntype", IsNullable = true)]
        public int? NetbiosNType { get; set; }  // Nullable int

        [XmlElement("netbios_scope", IsNullable = true)]
        public string NetbiosScope { get; set; }

        [XmlElement("create_gw", IsNullable = true)]
        public string CreateGW { get; set; }

        [XmlElement("verbosity_level", IsNullable = true)]
        public int? VerbosityLevel { get; set; }  // Nullable int

        [XmlElement("data_ciphers", IsNullable = true)]
        public string DataCiphers { get; set; }

        [XmlElement("ping_method", IsNullable = true)]
        public string PingMethod { get; set; }

        [XmlElement("keepalive_interval", IsNullable = true)]
        public int? KeepAliveInterval { get; set; }  // Nullable int

        [XmlElement("keepalive_timeout", IsNullable = true)]
        public int? KeepAliveTimeout { get; set; }  // Nullable int

        [XmlElement("ping_seconds", IsNullable = true)]
        public int? PingSeconds { get; set; }  // Nullable int

        [XmlElement("ping_push", IsNullable = true)]
        public string PingPush { get; set; }

        [XmlElement("ping_action", IsNullable = true)]
        public string PingAction { get; set; }

        [XmlElement("ping_action_seconds", IsNullable = true)]
        public int? PingActionSeconds { get; set; }  // Nullable int

        [XmlElement("ping_action_push", IsNullable = true)]
        public string PingActionPush { get; set; }

        [XmlElement("inactive_seconds", IsNullable = true)]
        public int? InactiveSeconds { get; set; }  // Nullable int
    }
}
