namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Interface
    {
        public string If { get; set; }  // Interface name (e.g., 'em0')
        public bool Enable { get; set; }
        public string Description { get; set; }
        public string SpoofMAC { get; set; }
        public string IPAddr { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string BlockPriv { get; set; }
        public string BlockBogons { get; set; }
    }
}
