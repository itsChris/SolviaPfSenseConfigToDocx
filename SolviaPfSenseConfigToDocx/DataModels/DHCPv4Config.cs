namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class DHCPv4Config
    {
        public DHCPRange Range { get; set; }
        public string FailoverPeerIP { get; set; }
        public int DefaultLeaseTime { get; set; }
        public int MaxLeaseTime { get; set; }
        public string Netmask { get; set; }
        public string Gateway { get; set; }
        public string Domain { get; set; }
        public List<string> DomainSearchList { get; set; } = new List<string>();
        public DDNSSettings DDNS { get; set; }
        public List<StaticMapping> StaticMappings { get; set; } = new List<StaticMapping>();
        public string TFTP { get; set; }
        public string NextServer { get; set; }
        public string Filename { get; set; }
        public string RootPath { get; set; }
        public int NumberOptions { get; set; }
        public bool Enable { get; set; }
    }
}
