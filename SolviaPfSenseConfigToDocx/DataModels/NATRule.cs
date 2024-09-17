namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class NATRule
    {
        public RuleEndpoint Source { get; set; } = new RuleEndpoint();
        public RuleEndpoint Target { get; set; } = new RuleEndpoint();
        public string Interface { get; set; }
        public string PoolOpts { get; set; }
        public string SourceHashKey { get; set; }
        public string Gateway { get; set; }
        public string IPProtocol { get; set; }
        public string Description { get; set; }
        public Timestamp Created { get; set; }
        public Timestamp Updated { get; set; }
    }
}
