namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class DDNSSettings
    {
        public string DDNSDomain { get; set; }
        public string DDNSPrimary { get; set; }
        public int DDNSPrimaryPort { get; set; }
        public string DDNSSecondary { get; set; }
        public int DDNSSecondaryPort { get; set; }
        public string DDNSKeyName { get; set; }
        public string DDNSKeyAlgorithm { get; set; }
        public string DDNSKey { get; set; }
    }
}
