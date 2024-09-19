namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class CertificateConfig
    {
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
        public List<CertificateAuthority> CertificateAuthorities { get; set; } = new List<CertificateAuthority>();
    }
}
