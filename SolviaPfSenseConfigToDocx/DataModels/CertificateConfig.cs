namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class CertificateConfig
    {
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
        public List<CA> CertificateAuthorities { get; set; } = new List<CA>();
    }
}
