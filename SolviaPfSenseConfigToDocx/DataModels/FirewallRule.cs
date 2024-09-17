namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class FirewallRule
    {
        public string ID { get; set; }
        public string Tracker { get; set; }
        public string Type { get; set; }
        public string Interface { get; set; }
        public string IPProtocol { get; set; }
        public RuleEndpoint Source { get; set; } = new RuleEndpoint();
        public RuleEndpoint Destination { get; set; } = new RuleEndpoint();
        public int? Max { get; set; }
        public int? MaxSrcNodes { get; set; }
        public int? MaxSrcConn { get; set; }
        public int? MaxSrcStates { get; set; }
        public int? Statetimeout { get; set; }
        public string Statetype { get; set; }
        public string OS { get; set; }
        public string Protocol { get; set; }
        public bool Log { get; set; }
        public bool Disabled { get; set; }
        public string Description { get; set; }
        public Timestamp Created { get; set; }
        public Timestamp Updated { get; set; }
    }
}
