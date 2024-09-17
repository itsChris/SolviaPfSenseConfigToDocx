namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Package
    {
        public string Name { get; set; }           // Package name
        public string Version { get; set; }        // Package version
        public string Description { get; set; }    // Description of the package
        public string Website { get; set; }        // Link to package website
        public string PkgInfoLink { get; set; }    // Link to package information
        public string ConfigurationFile { get; set; } // Reference to configuration file
        public Logging Logging { get; set; }       // Logging details (new addition)
        public string IncludeFile { get; set; }    // Include file for the package
    }

}
