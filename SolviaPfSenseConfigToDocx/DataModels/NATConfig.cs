namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class NATConfig
    {
        public string Mode { get; set; }
        public List<NATRule> NATRules { get; set; } = new List<NATRule>();
    }
}
