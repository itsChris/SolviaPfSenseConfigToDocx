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
            foreach (var interfaceConfig in interfaces)
            {
                DocumentHelper.AddHeading(body, interfaceConfig.Description, 2, mainDocumentPart);
                DocumentHelper.AddTableFromObject(body, interfaceConfig);
            }
        }
        public static void AddSystemConfigToDocument(SystemConfig systemConfig, Body body, MainDocumentPart mainDocumentPart)
        {
            DocumentHelper.AddHeading(body, "General Settings", 2, mainDocumentPart);

            // Add table from systemConfig object to document
            DocumentHelper.AddTableFromObject(body, systemConfig);

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
            foreach (var rule in firewallConfig.FirewallRules)
            {
                DocumentHelper.AddHeading(body, $"Rule: {rule.Description}", 2, mainPart);
                DocumentHelper.AddParagraph(body, $"Created at: {rule.Created.Time} by {rule.Created.Username}");
                DocumentHelper.AddParagraph(body, $"Updated at: {rule.Updated.Time} by {rule.Updated.Username}");
                DocumentHelper.AddTableFromObject(body, rule);
            }
        }

        internal static void AddCertificatesAndCAToDocument(CertificateConfig certificatesAndCA, Body body, MainDocumentPart mainPart)
        {
            foreach (var cert in certificatesAndCA.Certificates)
            {
                DocumentHelper.AddHeading(body, cert.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, cert);
            }
            foreach (var ca in certificatesAndCA.CertificateAuthorities)
            {
                DocumentHelper.AddHeading(body, ca.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, ca);
            }
        }

        internal static void AddUsersToDocument(List<User> users, Body body, MainDocumentPart mainPart)
        {
            foreach (var item in users)
            {
                DocumentHelper.AddHeading(body, item.Name, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, item);
            }
        }

        internal static void AddGroupsToDocument(List<Group> groups, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, groups);
        }

        internal static void AddOtherConfigsToDocument(OtherConfigurations otherConfigs, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, otherConfigs);
        }

        internal static void AddPackagesToDocument(List<Package> packages, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, packages);
        }

        internal static void AddVirtualIPsToDocument(List<VirtualIP> virtualIPs, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, virtualIPs);
        }

        internal static void AddServicesToDocument(List<Service> services, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, services);
        }

        internal static void AddGatewaysToDocument(List<Gateway> gateways, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, gateways);
        }

        internal static void AddAliasesToDocument(List<Alias> aliases, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, aliases);
        }

        internal static void AddCronJobsToDocument(List<CronJob> cronJobs, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, cronJobs);
        }

        internal static void AddIpSecVpnConfigToDocument(List<IpSecConnection> ipSecConnections, Body body, MainDocumentPart mainPart)
        {
            foreach (var ipSecConnection in ipSecConnections)
            {
                DocumentHelper.AddHeading(body, ipSecConnection.Phase1.Description, 2, mainPart);
                DocumentHelper.AddTableFromObject(body, ipSecConnection.Phase1);
                foreach (var phase2 in ipSecConnection.Phase2List)
                {
                    DocumentHelper.AddHeading(body, phase2.Description, 3, mainPart);
                    DocumentHelper.AddTableFromObject(body, phase2);
                }
            }
        }
    }
}
