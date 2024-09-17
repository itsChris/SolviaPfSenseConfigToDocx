namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class StaticMapping
    {
        public string MAC { get; set; }
        public string CID { get; set; }
        public string IPAddr { get; set; }
        public string Hostname { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string RootPath { get; set; }
        public int DefaultLeaseTime { get; set; }
        public int MaxLeaseTime { get; set; }
        public string Gateway { get; set; }
        public string Domain { get; set; }
        public List<string> DomainSearchList { get; set; } = new List<string>();
        public DDNSSettings DDNS { get; set; }
        public string TFTP { get; set; }
        public string NextServer { get; set; }
    }
}
