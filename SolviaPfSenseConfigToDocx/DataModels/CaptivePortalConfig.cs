namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class CaptivePortalConfig
    {
        public string Interface { get; set; }     // Interface where the captive portal is active
        public int MaxClients { get; set; }       // Maximum number of clients
        public int IdleTimeout { get; set; }      // Timeout for idle connections (in seconds)
        public int HardTimeout { get; set; }      // Hard limit on session duration (in seconds)
        public string AuthenticationMethod { get; set; } // Captive portal authentication method
        public bool Enable { get; set; }          // Captive portal enabled or disabled
    }

}
