namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Alias
    {
        public string AliasName { get; set; }   // Name of the alias
        public string Type { get; set; }        // Type of alias (e.g., host, network, port)
        public List<string> Address { get; set; } = new List<string>();  // List of IP addresses or networks
        public string Description { get; set; } // Description of the alias
        public List<string> Detail { get; set; } = new List<string>();   // Additional details, like hostname or port
    }

}
