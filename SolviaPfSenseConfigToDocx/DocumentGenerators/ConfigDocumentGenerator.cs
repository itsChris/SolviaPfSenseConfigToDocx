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
            // Add table from systemConfig object to document
            DocumentHelper.AddTableFromObject(body, interfaces);

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
            DocumentHelper.AddTableFromObject(body, dhcpConfig);
        }

        internal static void AddFirewallConfigToDocument(FirewallConfig firewallConfig, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, firewallConfig);
        }

        internal static void AddCertificatesAndCAToDocument(CertificateConfig certificatesAndCA, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, certificatesAndCA);
        }

        internal static void AddUsersToDocument(List<User> users, Body body, MainDocumentPart mainPart)
        {
            DocumentHelper.AddTableFromObject(body, users);
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
            DocumentHelper.AddTableFromObject(body, ipSecConnections);
        }
    }
}
