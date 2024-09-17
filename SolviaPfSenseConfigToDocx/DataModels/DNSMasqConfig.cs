namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class DNSMasqConfig
    {
        public bool Enable { get; set; }              // DNSMasq enabled or disabled
        public bool DNSSEC { get; set; }              // Enable DNSSEC validation
        public List<string> DomainOverride { get; set; } = new List<string>(); // Domain overrides for DNS queries
        public List<string> HostsOverride { get; set; } = new List<string>();  // Static host mappings for DNS
    }

}
