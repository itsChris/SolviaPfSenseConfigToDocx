namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class StaticRoute
    {
        public string Destination { get; set; }  // The destination network or IP address (e.g., 192.168.1.0/24)
        public string Gateway { get; set; }      // The gateway through which the traffic should be routed
        public string Interface { get; set; }    // The network interface associated with the route
        public string Description { get; set; }  // A description for the route
        public bool Disabled { get; set; }       // Indicates whether the route is disabled
    }
}
