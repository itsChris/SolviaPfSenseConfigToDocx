namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class IPsecPhase2
    {
        public int IKEID { get; set; }
        public string UniqID { get; set; }
        public string Mode { get; set; }
        public string ReqID { get; set; }
        public VPNID LocalID { get; set; }
        public VPNID RemoteID { get; set; }
        public string Protocol { get; set; }
        public List<EncryptionAlgorithmOption> EncryptionAlgorithmOptions { get; set; } = new List<EncryptionAlgorithmOption>();
        public string HashAlgorithmOption { get; set; }
        public string PFSGroup { get; set; }
        public int Lifetime { get; set; }
        public int RekeyTime { get; set; }
        public int RandTime { get; set; }
        public string PingHost { get; set; }
        public bool Keepalive { get; set; }
        public string Description { get; set; }
    }
}
