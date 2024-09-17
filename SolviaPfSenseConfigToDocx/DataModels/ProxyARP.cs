namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class ProxyARP
    {
        public string Interface { get; set; }     // Interface where Proxy ARP is active
        public List<string> Subnet { get; set; } = new List<string>();  // Subnets covered by Proxy ARP
        public bool Enable { get; set; }          // Proxy ARP enabled or disabled
    }

}
