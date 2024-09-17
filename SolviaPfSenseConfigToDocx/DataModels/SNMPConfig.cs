namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class SNMPConfig
    {
        public string SysLocation { get; set; }   // Location information for SNMP
        public string SysContact { get; set; }    // Contact information for SNMP
        public string ROCommunity { get; set; }   // Read-only community string
        public string TrapServer { get; set; }    // Trap server IP
        public string TrapString { get; set; }    // Trap community string
        public string Version { get; set; }       // SNMP version
        public bool Enable { get; set; }          // SNMP enabled or disabled
    }

}
