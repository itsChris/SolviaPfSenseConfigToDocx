using SolviaPfSenseConfigToDocx.CustomAttributes;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class IPsecPhase1
    {
        public string AuthenticationMethod { get; set; }
        public string Caref { get; set; }
        public string CertRef { get; set; }
        public string CloseAction { get; set; }
        public string Description { get; set; }
        public string DHGroup { get; set; }
        public int DPDDelay { get; set; }
        public int DPDMaxFail { get; set; }
        public List<EncryptionAlgorithm> EncryptionAlgorithms { get; set; } = new List<EncryptionAlgorithm>();
        public string HashAlgorithm { get; set; }
        public int IKEID { get; set; }
        public string IKEType { get; set; }
        public string Interface { get; set; }
        public int Lifetime { get; set; }
        public bool MOBIKE { get; set; }
        public string MyIDType { get; set; }
        public bool NATTraversal { get; set; }
        public string PeerIDType { get; set; }
        public string PKCS11CertRef { get; set; }
        public string PKCS11Pin { get; set; }
        [Secret(true)]
        public string PreSharedKey { get; set; }

        public string PRFAlgorithm { get; set; }
        public string PrivateKey { get; set; }
        public string Protocol { get; set; }
        public int RandTime { get; set; }
        public int RekeyTime { get; set; }
        public string RemoteGateway { get; set; }
        public string StartAction { get; set; }
    }
}
