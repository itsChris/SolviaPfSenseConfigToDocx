namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class IpSecConnection
    {
        public IPsecPhase1 Phase1 { get; set; }
        public List<IPsecPhase2> Phase2List { get; set; } = new List<IPsecPhase2>();
    }



}
