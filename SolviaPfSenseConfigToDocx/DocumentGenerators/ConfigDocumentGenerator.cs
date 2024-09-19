using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Helpers;

namespace SolviaPfSenseConfigToDocx.DocumentGenerators
{
    public static class ConfigDocumentGenerator
    {
        public static void GenerateDocument(SystemConfig systemConfig, IpSecVPNConfig vpnConfig, string outputPath)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(outputPath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                // Add a main document part
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Title Page
                DocumentHelper.AddTitlePage(body, "Configuration Documentation");

                // Table of Contents
                DocumentHelper.AddTableOfContents(body, mainPart);

                // Add SystemConfig Section
                DocumentHelper.AddHeading(body, "System Configuration", 1, mainPart);
                AddSystemConfigToDocument(systemConfig, body, mainPart);

                // Add VPNConfig Section
                DocumentHelper.AddHeading(body, "IPSec VPN Configuration", 1, mainPart);
                AddIpSecVpnConfigToDocument(vpnConfig, body, mainPart);

                // Finalize document
                mainPart.Document.Save();
            }
        }

        public static void AddInterfaceConfigToDocument(List<Interface> interfaces, Body body, MainDocumentPart mainDocumentPart)
        {
            DocumentHelper.AddHeading(body, "General Settings", 2, mainDocumentPart);

            // Add table from systemConfig object to document
            DocumentHelper.AddTableFromObject(body, interfaces);

        }

        public static void AddSystemConfigToDocument(SystemConfig systemConfig, Body body, MainDocumentPart mainDocumentPart)
        {
            DocumentHelper.AddHeading(body, "General Settings", 2, mainDocumentPart);

            // Add table from systemConfig object to document
            DocumentHelper.AddTableFromObject(body, systemConfig);

            return;
            DocumentHelper.AddParagraph(body, "Hostname", systemConfig.Hostname);
            DocumentHelper.AddParagraph(body, $"Hostname: {systemConfig.Hostname}");
            DocumentHelper.AddParagraph(body, $"Domain: {systemConfig.Domain}");
            DocumentHelper.AddParagraph(body, $"NextUID: {systemConfig.NextUID}");
            DocumentHelper.AddParagraph(body, $"NextGID: {systemConfig.NextGID}");

            DocumentHelper.AddHeading(body, "Time Servers", 2, mainDocumentPart);
            foreach (var timeServer in systemConfig.Timeservers)
            {
                DocumentHelper.AddParagraph(body, $"Time Server: {timeServer}");
            }

            DocumentHelper.AddHeading(body, "DNS Settings", 2, mainDocumentPart);
            foreach (var dnsServer in systemConfig.DNSServers)
            {
                DocumentHelper.AddParagraph(body, $"DNS Server: {dnsServer}");
            }
            DocumentHelper.AddParagraph(body, $"DNS Allow Override: {systemConfig.DNSAllowOverride}");

            DocumentHelper.AddHeading(body, "Power Settings", 2, mainDocumentPart);
            DocumentHelper.AddParagraph(body, $"AC Mode: {systemConfig.PowerSettings.PowerdAcMode}");
            DocumentHelper.AddParagraph(body, $"Battery Mode: {systemConfig.PowerSettings.PowerdBatteryMode}");
            DocumentHelper.AddParagraph(body, $"Normal Mode: {systemConfig.PowerSettings.PowerdNormalMode}");

            // Add other necessary system config details similarly
        }

        public static void AddIpSecVpnConfigToDocument(IpSecVPNConfig vpnConfig, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddHeading(body, "IPsec Phase 1 Configurations", 2, mainPart);
            foreach (var phase1 in vpnConfig.IPsecPhase1Configs)
            {
                DocumentHelper.AddParagraph(body, $"IKE ID: {phase1.IKEID}");
                DocumentHelper.AddParagraph(body, $"Remote Gateway: {phase1.RemoteGateway}");
                DocumentHelper.AddParagraph(body, $"Pre-Shared Key: {phase1.PreSharedKey}");
                DocumentHelper.AddParagraph(body, $"Cert Ref: {phase1.CertRef}");
                DocumentHelper.AddHeading(body, "Encryption Algorithms", 3, mainPart);
                foreach (var algorithm in phase1.EncryptionAlgorithms)
                {
                    DocumentHelper.AddParagraph(body, $"Algorithm Name: {algorithm.Name}, Key Length: {algorithm.KeyLength}");
                }
                DocumentHelper.AddParagraph(body, $"Hash Algorithm: {phase1.HashAlgorithm}");
            }

            DocumentHelper.AddHeading(body, "IPsec Phase 2 Configurations", 2, mainPart);
            foreach (var phase2 in vpnConfig.IPsecPhase2Configs)
            {
                DocumentHelper.AddParagraph(body, $"Mode: {phase2.Mode}");
                DocumentHelper.AddParagraph(body, $"Local ID: {phase2.LocalID.Address}");
                DocumentHelper.AddParagraph(body, $"Remote ID: {phase2.RemoteID.Address}");
                DocumentHelper.AddHeading(body, "Encryption Algorithm Options", 3, mainPart);
                foreach (var option in phase2.EncryptionAlgorithmOptions)
                {
                    DocumentHelper.AddParagraph(body, $"Algorithm: {option.Name}, Key Length: {option.KeyLength}");
                }
            }
        }
    }
}
