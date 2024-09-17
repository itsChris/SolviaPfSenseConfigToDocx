namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class VirtualIP
    {
        public string Mode { get; set; }         // Mode of the virtual IP (e.g., CARP, IP Alias, Proxy ARP)
        public string Interface { get; set; }    // Interface associated with the virtual IP
        public string Address { get; set; }      // Virtual IP address
        public int SubnetBits { get; set; }      // Subnet bits (e.g., 24)
        public string Description { get; set; }  // Description of the virtual IP
    }

}
