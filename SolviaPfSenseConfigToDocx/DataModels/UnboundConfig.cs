namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class UnboundConfig
    {
        public bool Enable { get; set; }
        public bool DNSSEC { get; set; }
        public List<string> DomainOverride { get; set; } = new List<string>();
        public List<string> HostsOverride { get; set; } = new List<string>();
        public bool ForwardingMode { get; set; }

        // New fields
        public bool HideIdentity { get; set; }  // Added
        public bool HideVersion { get; set; }   // Added
        public bool DnsSecStripped { get; set; } // Added
        public string CustomOptions { get; set; } // Added
    }


}
