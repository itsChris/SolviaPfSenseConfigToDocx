namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Gateway
    {
        public string Interface { get; set; }
        public string GatewayAddress { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string IPProtocol { get; set; }
        public string Description { get; set; }
        public bool DefaultGateway { get; set; }
        public bool GWDownKillStates { get; set; }  // Added
    }
}
