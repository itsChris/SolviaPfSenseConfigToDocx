namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class OpenVPNConfig
    {
        public int VPNID { get; set; }
        public string Mode { get; set; }
        public string AuthMode { get; set; }
        public string Protocol { get; set; }
        public string Interface { get; set; }
        public string IPAddress { get; set; }
        public int LocalPort { get; set; }
        public string TunnelNetwork { get; set; }
        public string RemoteNetwork { get; set; }
        public int MaxClients { get; set; }
        public bool ClientToClient { get; set; }
        public List<string> DNSServers { get; set; } = new List<string>();
        public string DNSDomain { get; set; }
        public TLSConfig TLS { get; set; }
        public CompressionConfig Compression { get; set; }
        public bool ExitNotify { get; set; }

        public ServerConfig ServerConfig { get; set; } // Added

    }
}
