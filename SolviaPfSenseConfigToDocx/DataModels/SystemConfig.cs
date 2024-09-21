namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class SystemConfig
    {
        public string AllreadyRunConfigUpgrade { get; set; }
        public string BogonsIntervall { get; set; }
        public bool DisableLargeReceiveOffloading { get; set; }
        public bool DisableNatReflection { get; set; }
        public bool DisableSegmentationOffloading { get; set; }
        public string DNS1GW { get; set; }
        public string DNS2GW { get; set; }
        public bool DNSAllowOverride { get; set; }
        public List<string> DNSServers { get; set; } = new List<string>();
        public string Domain { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
        public string HnAltqEnable { get; set; }
        public string Hostname { get; set; }
        public bool IPv6Allow { get; set; }
        public string Language { get; set; }
        public int MaximumFrags { get; set; }
        public int MaximumStates { get; set; }
        public int MaximumTableEntries { get; set; }
        public int NextGID { get; set; }
        public int NextUID { get; set; }
        public string Optimization { get; set; }
        public PowerSettings PowerSettings { get; set; }
        public string PrimaryConsole { get; set; }
        public int ReflectionTimeout { get; set; }
        public int SerialSpeed { get; set; }
        public SSHConfig SSH { get; set; }
        public string SshState { get; set; }
        public List<string> Timeservers { get; set; } = new List<string>();
        public string Timezone { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public WebGUIConfig WebGUI { get; set; }
    }
}
