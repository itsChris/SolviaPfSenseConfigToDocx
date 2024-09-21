using SolviaPfSenseConfigToDocx.CustomAttributes;
using System.Security.Cryptography.X509Certificates;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class Certificate
    {
        public X509Certificate2 Certi { get;set; }
        // Properties from the SystemConfig context (e.g., user-related certificates)
        public string UID { get; set; }         // Unique ID for user-related certificates
        [Exclude(true)]
        public string Cert { get; set; }        // Certificate content (CRT)
        public string Description { get; set; } // Description of the certificate

        // Properties from the Certificates/CA context
        public string RefID { get; set; }       // Reference ID for general certificates
        public string Type { get; set; }        // Certificate type (e.g., server, user)
        [Exclude(true)]
        public string PrivateKey { get; set; }         // Private key (PRV)
        public string CARef { get; set; }       // Reference to the Certificate Authority (CA) if applicable
    }
}
