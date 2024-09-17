namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Group
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public int GID { get; set; }
        public List<string> Members { get; set; } = new List<string>();
        public List<string> Privileges { get; set; } = new List<string>();
    }
}
