namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class FirewallConfig
    {
        public List<FirewallRule> FirewallRules { get; set; } = new List<FirewallRule>();
        public List<NATRule> NATRules { get; set; } = new List<NATRule>();  // Add this property
    }

}
