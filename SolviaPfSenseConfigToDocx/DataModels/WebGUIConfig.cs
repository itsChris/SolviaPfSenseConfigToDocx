namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class WebGUIConfig
    {
        public string Protocol { get; set; }
        public bool LoginAutocomplete { get; set; }
        public string SSLCertRef { get; set; }
        public int DashboardColumns { get; set; }
        public string WebGUICSS { get; set; }
        public string LoginCSS { get; set; }
        public int MaxProcesses { get; set; }
        public bool Roaming { get; set; }
        public bool WebGUIFixedMenu { get; set; }
        public bool WebGUIHostnameMenu { get; set; }
        public bool LoginShowHost { get; set; }
    }
}
