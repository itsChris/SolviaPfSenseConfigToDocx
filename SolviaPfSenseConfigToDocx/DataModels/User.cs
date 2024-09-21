using SolviaPfSenseConfigToDocx.CustomAttributes;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class User
    {
        public List<string> AuthorizedKeys { get; set; } = new List<string>();
        [Secret(true)]
        public string BcryptHash { get; set; }
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
        public int DashboardColumns { get; set; }
        public string Description { get; set; }
        public DateTime? Expires { get; set; }
        public string Groupname { get; set; }
        public string IPSecPSK { get; set; }
        public string Name { get; set; }
        public List<string> Privileges { get; set; } = new List<string>();
        public string Scope { get; set; }
        public int UID { get; set; }
        public string WebGUICSS { get; set; }
    }
}
