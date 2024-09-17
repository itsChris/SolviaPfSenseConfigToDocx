namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class User
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public string Groupname { get; set; }
        public int UID { get; set; }
        public List<string> Privileges { get; set; } = new List<string>();
        public string BcryptHash { get; set; }
        public DateTime? Expires { get; set; }
        public List<string> AuthorizedKeys { get; set; } = new List<string>();
        public string IPSecPSK { get; set; }
        public string WebGUICSS { get; set; }
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
