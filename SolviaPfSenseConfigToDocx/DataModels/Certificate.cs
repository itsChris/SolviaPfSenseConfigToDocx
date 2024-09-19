namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Certificate
    {
        // Properties from the SystemConfig context (e.g., user-related certificates)
        public string UID { get; set; }         // Unique ID for user-related certificates
        public string Cert { get; set; }        // Certificate content (CRT)
        public string Description { get; set; } // Description of the certificate

        // Properties from the Certificates/CA context
        public string RefID { get; set; }       // Reference ID for general certificates
        public string Type { get; set; }        // Certificate type (e.g., server, user)
        public string PrivateKey { get; set; }         // Private key (PRV)
        public string CARef { get; set; }       // Reference to the Certificate Authority (CA) if applicable
    }
}
