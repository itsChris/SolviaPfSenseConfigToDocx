namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class SystemConfig
    {
        public string Optimization { get; set; }
        public string Hostname { get; set; }
        public string Domain { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<User> Users { get; set; } = new List<User>();
        public int NextUID { get; set; }
        public int NextGID { get; set; }
        public List<string> Timeservers { get; set; } = new List<string>();
        public WebGUIConfig WebGUI { get; set; }
        public bool DisableSegmentationOffloading { get; set; }
        public bool DisableLargeReceiveOffloading { get; set; }
        public bool IPv6Allow { get; set; }
        public int MaximumTableEntries { get; set; }
        public PowerSettings PowerSettings { get; set; }
        public SSHConfig SSH { get; set; }
        public List<string> DNSServers { get; set; } = new List<string>();
        public bool DNSAllowOverride { get; set; }
        public string DNS1GW { get; set; }
        public string DNS2GW { get; set; }
        public int SerialSpeed { get; set; }
        public string PrimaryConsole { get; set; }
        public int MaximumStates { get; set; }
        public int MaximumFrags { get; set; }
        public int ReflectionTimeout { get; set; }
    }
}
