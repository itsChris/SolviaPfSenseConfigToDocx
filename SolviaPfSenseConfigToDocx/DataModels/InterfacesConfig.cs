namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class InterfacesConfig
    {
        public Interface WAN { get; set; }
        public Interface LAN { get; set; }
        public List<Interface> OptionalInterfaces { get; set; } = new List<Interface>();
    }
}
