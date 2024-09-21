using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Helpers;

namespace SolviaPfSenseConfigToDocx.DocumentGenerators
{
    public static class ConfigDocumentGenerator
    {
        public static void AddInterfaceConfigToDocument(List<Interface> interfaces, Body body, MainDocumentPart mainDocumentPart)
        {
            if (interfaces.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No interfaces configured.");
            }

            foreach (var interfaceConfig in interfaces)
            {
                DocumentHelper.AddHeading(body, interfaceConfig.Description, 2, mainDocumentPart);
                DocumentHelper.AddTableFromObject(body, interfaceConfig);
            }
        }
        public static void AddSystemConfigToDocument(SystemConfig systemConfig, Body body, MainDocumentPart mainDocumentPart)
        {
            DocumentHelper.AddHeading(body, "General Settings", 2, mainDocumentPart);

            DocumentHelper.AddTableFromObject(body, systemConfig);
            DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
            DocumentHelper.AddHeading(body, "Web GUI Settings", 2, mainDocumentPart);
            DocumentHelper.AddTableFromObject(body, systemConfig.WebGUI);
        }
        internal static void AddStaticRoutesToDocument(List<StaticRoute> staticRoutes, Body body, MainDocumentPart mainPart)
        {
            if (staticRoutes.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No static routes configured.");
            }
            DocumentHelper.AddTableFromObject(body, staticRoutes);
        }

        internal static void AddDhcpConfigToDocument(DHCPConfig dhcpConfig, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddHeading(body, "DHCPv4 Configuration", 2, mainPart);
            DocumentHelper.AddTableFromObject(body, dhcpConfig.DHCPv4);
            foreach (var sm in dhcpConfig.DHCPv4.StaticMappings)
            {
                string headingText = !string.IsNullOrEmpty(sm.Description) ? sm.Description :
                     !string.IsNullOrEmpty(sm.Hostname) ? sm.Hostname :
                     "n/a";

                DocumentHelper.AddHeading(body, $"Static reservation for: {headingText}", 3, mainPart);

                DocumentHelper.AddTableFromObject(body, sm);
            }
        }

        internal static void AddFirewallConfigToDocument(FirewallConfig firewallConfig, Body body, MainDocumentPart mainPart)
        {

            if (firewallConfig.FirewallRules.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No firewall rules configured.");
            }

            foreach (var rule in firewallConfig.FirewallRules)
            {
                DocumentHelper.AddHeading(body, $"Rule: {rule.Description}", 2, mainPart);
                DocumentHelper.AddParagraph(body, $"Created at: {rule.Created.Time} by {rule.Created.Username}");
                DocumentHelper.AddParagraph(body, $"Updated at: {rule.Updated.Time} by {rule.Updated.Username}");
                DocumentHelper.AddTableFromObject(body, rule);
                DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
            }
        }

        internal static void AddCertificatesAndCAToDocument(CertificateConfig certificatesAndCA, Body body, MainDocumentPart mainPart)
        {
            if (certificatesAndCA.Certificates.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No certificates configured.");
            }

            if (certificatesAndCA.CertificateAuthorities.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No certificate authorities configured.");
            }

            foreach (var cert in certificatesAndCA.Certificates)
            {
                DocumentHelper.AddHeading(body, cert.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, cert);
                DocumentHelper.AddHeading(body, $"Certificate details for: {cert.Description}", 3, mainPart);
                DocumentHelper.AddTableFromObject(body, cert.Certi);
            }
            foreach (var ca in certificatesAndCA.CertificateAuthorities)
            {
                DocumentHelper.AddHeading(body, ca.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, ca);
            }
        }

        internal static void AddUsersToDocument(List<User> users, Body body, MainDocumentPart mainPart)
        {
            if (users.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No users configured.");
            }

            foreach (var item in users)
            {
                DocumentHelper.AddHeading(body, item.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, item);
            }
        }

        internal static void AddGroupsToDocument(List<Group> groups, Body body, MainDocumentPart mainPart)
        {
            if (groups.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No groups configured.");
            }

            foreach (var item in groups)
            { 
                DocumentHelper.AddHeading(body, item.Name, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, item);
            }
        }

        internal static void AddOtherConfigsToDocument(OtherConfigurations otherConfigs, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, otherConfigs);
        }

        internal static void AddPackagesToDocument(List<Package> packages, Body body, MainDocumentPart mainPart)
        {
            if (packages.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No packages installed.");
            }
            foreach (var package in packages)
            {
                DocumentHelper.AddHeading(body, package.Name, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, package);
            }
        }

        internal static void AddVirtualIPsToDocument(List<VirtualIP> virtualIPs, Body body, MainDocumentPart mainPart)
        {
            if (virtualIPs.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No virtual IPs configured.");
            }

            DocumentHelper.AddTableFromObject(body, virtualIPs);
        }

        internal static void AddServicesToDocument(List<Service> services, Body body, MainDocumentPart mainPart)
        {
            if (services.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No services configured.");
            }

            foreach (var service in services)
            {
                DocumentHelper.AddHeading(body, service.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, service);
            }
        }

        internal static void AddGatewaysToDocument(List<Gateway> gateways, Body body, MainDocumentPart mainPart)
        {
            if (gateways.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No gateways configured.");
            }

            foreach (var gateway in gateways)
            { 
                DocumentHelper.AddHeading(body, gateway.Name, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, gateway);
            }
        }

        internal static void AddAliasesToDocument(List<Alias> aliases, Body body, MainDocumentPart mainPart)
        {

            if (aliases.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No aliases configured.");
            }

            DocumentHelper.AddTableFromObject(body, aliases);
        }

        internal static void AddCronJobsToDocument(List<CronJob> cronJobs, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, cronJobs);
        }

        internal static void AddIpSecVpnConfigToDocument(List<IpSecConnection> ipSecConnections, Body body, MainDocumentPart mainPart)
        {
            if(ipSecConnections.Count == 0)
            {
                DocumentHelper.AddParagraph(body, "No IPsec VPN connections configured.");
            }

            foreach (var ipSecConnection in ipSecConnections)
            {
                DocumentHelper.AddHeading(body, $"{ipSecConnection.Phase1.Description} (Phase 1 (IKE Phase 1))", 2, mainPart);
                DocumentHelper.AddTableFromObject(body, ipSecConnection.Phase1);
                foreach (var phase2 in ipSecConnection.Phase2List)
                {
                    DocumentHelper.AddHeading(body, $"{phase2.Description} (Phase 2 (IKE Phase 2) - Security Association", 3, mainPart);
                    DocumentHelper.AddTableFromObject(body, phase2);
                }
                DocumentHelper.InsertSectionBreak(body,SectionMarkValues.NextPage);
            }
        }
    }
}
