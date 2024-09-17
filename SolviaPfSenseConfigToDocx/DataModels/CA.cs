namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class CA
    {
        public string RefID { get; set; }        // Reference ID for the CA
        public string Description { get; set; }  // Description of the CA
        public bool Trust { get; set; }          // Whether the CA is trusted
        public string CRT { get; set; }          // CA certificate content
        public string PRV { get; set; }          // Private key for the CA
        public bool RandomSerial { get; set; }   // Random serial number flag
        public int Serial { get; set; }          // Serial number for the CA
    }
}
