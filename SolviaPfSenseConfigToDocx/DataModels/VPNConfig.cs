namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class VPNConfig
    {
        public List<IPsecPhase1> IPsecPhase1Configs { get; set; } = new List<IPsecPhase1>();
        public List<IPsecPhase2> IPsecPhase2Configs { get; set; } = new List<IPsecPhase2>();
        public List<OpenVPNConfig> OpenVPNConfigs { get; set; } = new List<OpenVPNConfig>();
    }
}
