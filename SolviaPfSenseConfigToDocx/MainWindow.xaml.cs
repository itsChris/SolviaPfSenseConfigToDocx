﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.DocumentGenerators;
using SolviaPfSenseConfigToDocx.Helpers;
using SolviaPfSenseConfigToDocx.Parsers;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SolviaPfSenseConfigToDocx
{
    public partial class MainWindow : Window
    {
        private PfSenseConfigParser pfSenseConfigParser;

        private PfSense pfSense;
        private SystemConfig systemConfig;
        private List<Interface> interfaces;
        private List<StaticRoute> staticRoutes;
        private DHCPConfig dhcpConfig;
        private FirewallConfig firewallConfig;
        private CertificateConfig certificatesAndCA;
        private List<User> users;
        private List<Group> groups;
        private List<CronJob> cronJobs;
        private List<Gateway> gateways;
        private List<Alias> aliases;
        private List<VirtualIP> virtualIPs;
        private List<Package> packages;
        private List<Service> services;
        private List<IpSecConnection> ipSecConnections;
        private SyslogConfig sysLogConfig;
        private OpenVPNServerConfig openVPNServerConfig;
        private OtherConfigurations otherConfigs;

        private string lastUsedPath = string.Empty; // Store the last used path

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                pfSenseConfigParser = new PfSenseConfigParser(openFileDialog.FileName);

                // Parse each section
                pfSense = pfSenseConfigParser.PfSense();
                systemConfig = pfSenseConfigParser.ParseSystemConfig();
                users = pfSenseConfigParser.ParseUsers();
                groups = pfSenseConfigParser.ParseGroups();
                interfaces = pfSenseConfigParser.ParseInterfaces();
                staticRoutes = pfSenseConfigParser.ParseStaticRoutes();
                dhcpConfig = pfSenseConfigParser.ParseDHCPConfig();
                firewallConfig = pfSenseConfigParser.ParseFirewallRulesAndNAT();
                certificatesAndCA = pfSenseConfigParser.ParseCertificatesAndCA();
                cronJobs = pfSenseConfigParser.ParseCronJobs();
                gateways = pfSenseConfigParser.ParseGateways();
                aliases = pfSenseConfigParser.ParseAliases();
                virtualIPs = pfSenseConfigParser.ParseVirtualIPs();
                packages = pfSenseConfigParser.ParsePackages();
                services = pfSenseConfigParser.ParseServices();
                ipSecConnections = pfSenseConfigParser.ParseIpSecConnections();
                sysLogConfig = pfSenseConfigParser.ParseSysLogConfig();
                openVPNServerConfig = pfSenseConfigParser.ParseOpenVPNServerConfig();
                otherConfigs = pfSenseConfigParser.ParseOtherConfigurations();
            }
        }

        private void buttonGetClipboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if the clipboard contains text
                if (Clipboard.ContainsText())
                {
                    // Get the XML content from the clipboard
                    string clipboardXml = Clipboard.GetText();

                    // Save the clipboard XML to a temporary file
                    string tempFilePath = Path.GetTempFileName();
                    File.WriteAllText(tempFilePath, clipboardXml);

                    // Initialize the parser with the temporary file path
                    pfSenseConfigParser = new PfSenseConfigParser(tempFilePath);

                    // Parse each section (same as in SelectFileButton_Click)
                    pfSense = pfSenseConfigParser.PfSense();
                    systemConfig = pfSenseConfigParser.ParseSystemConfig();
                    users = pfSenseConfigParser.ParseUsers();
                    groups = pfSenseConfigParser.ParseGroups();
                    interfaces = pfSenseConfigParser.ParseInterfaces();
                    staticRoutes = pfSenseConfigParser.ParseStaticRoutes();
                    dhcpConfig = pfSenseConfigParser.ParseDHCPConfig();
                    firewallConfig = pfSenseConfigParser.ParseFirewallRulesAndNAT();
                    certificatesAndCA = pfSenseConfigParser.ParseCertificatesAndCA();
                    cronJobs = pfSenseConfigParser.ParseCronJobs();
                    gateways = pfSenseConfigParser.ParseGateways();
                    aliases = pfSenseConfigParser.ParseAliases();
                    virtualIPs = pfSenseConfigParser.ParseVirtualIPs();
                    packages = pfSenseConfigParser.ParsePackages();
                    services = pfSenseConfigParser.ParseServices();
                    ipSecConnections = pfSenseConfigParser.ParseIpSecConnections();
                    sysLogConfig = pfSenseConfigParser.ParseSysLogConfig();
                    openVPNServerConfig = pfSenseConfigParser.ParseOpenVPNServerConfig();
                    otherConfigs = pfSenseConfigParser.ParseOtherConfigurations();
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain valid XML content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading from the clipboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ExportToDocxButton_Click(object sender, RoutedEventArgs e)
        {
            if (pfSenseConfigParser == null)
            {
                MessageBox.Show("Please select a file first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Initialize SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = $"{DateTime.Now:yyyy-MM-dd-}{systemConfig.Hostname}-Documentation.docx",
                Filter = "Word Documents (*.docx)|*.docx",
                Title = "Save the Word Document"
            };

            // Set the initial directory to the last used path if available
            if (!string.IsNullOrEmpty(lastUsedPath))
            {
                saveFileDialog.InitialDirectory = lastUsedPath;
            }

            if (saveFileDialog.ShowDialog() == true)
            {

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(saveFileDialog.FileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {

                    // Add a main document part
                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    // Add Title Page
                    DocumentHelper.AddTitlePage(body, $"Firewall Documentation", "48");
                    DocumentHelper.AddTitlePage(body, $"for", "48");
                    DocumentHelper.AddTitlePage(body, $"{textboxCustomerName.Text}", "48");
                    DocumentHelper.AddTitlePage(body, $"Hostname: {systemConfig.Hostname}", "40");
                    DocumentHelper.AddTitlePage(body, $"pfSense Version: v{pfSense.Version}", "40");

                    DocumentHelper.AddTitlePage(body, $"Author: {textboxAuthor.Text}", "32");
                    DocumentHelper.AddTitlePage(body, $"Date: {datePicker.SelectedDate.Value.ToString("d")}", "32");
                    DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);

                    // Add ToC
                    DocumentHelper.AddTableOfContents(body, mainPart);
                    DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);

                    // Add System Configuration
                    if (chkSystemConfig.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "System Configuration", 1, mainPart);
                        ConfigDocumentGenerator.AddSystemConfigToDocument(systemConfig, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add IPSec VPN Configuration
                    if (chkIpSecVpnConfig.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "IPSec VPN Configuration", 1, mainPart);
                        ConfigDocumentGenerator.AddIpSecVpnConfigToDocument(ipSecConnections, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add Interfaces
                    if (chkInterfaces.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Interfaces", 1, mainPart);
                        ConfigDocumentGenerator.AddInterfaceConfigToDocument(interfaces, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add Static Routes
                    if (chkStaticRoutes.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Static Routes", 1, mainPart);
                        ConfigDocumentGenerator.AddStaticRoutesToDocument(staticRoutes, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add DHCP Configuration
                    if (chkDHCPConfig.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "DHCP Configuration", 1, mainPart);
                        ConfigDocumentGenerator.AddDhcpConfigToDocument(dhcpConfig, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add Firewall Configuration
                    if (chkFirewallConfig.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Firewall Configuration", 1, mainPart);
                        ConfigDocumentGenerator.AddFirewallConfigToDocument(firewallConfig, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Add Certificates and CA
                    if (chkCertificatesAndCA.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Certificates and CA", 1, mainPart);
                        ConfigDocumentGenerator.AddCertificatesAndCAToDocument(certificatesAndCA, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Users
                    if (chkUsers.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Users", 1, mainPart);
                        ConfigDocumentGenerator.AddUsersToDocument(users, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Groups
                    if (chkGroups.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Groups", 1, mainPart);
                        ConfigDocumentGenerator.AddGroupsToDocument(groups, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Packages
                    if (chkPackages.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Packages", 1, mainPart);
                        ConfigDocumentGenerator.AddPackagesToDocument(packages, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Services
                    if (chkServices.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Services", 1, mainPart);
                        ConfigDocumentGenerator.AddServicesToDocument(services, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Virtual IPs
                    if (chkVirtualIps.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Virtual IPs", 1, mainPart);
                        ConfigDocumentGenerator.AddVirtualIPsToDocument(virtualIPs, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // Gateways
                    if (chkGateways.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Gateways", 1, mainPart);
                        ConfigDocumentGenerator.AddGatewaysToDocument(gateways, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Aliases
                    if (chkAliases.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Aliases", 1, mainPart);
                        ConfigDocumentGenerator.AddAliasesToDocument(aliases, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Cron Jobs
                    if (chkCronJobs.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Cron Jobs", 1, mainPart);
                        ConfigDocumentGenerator.AddCronJobsToDocument(cronJobs, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // SysLog
                    if (chkSysLog.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "SysLog", 1, mainPart);
                        DocumentHelper.AddTableFromObject(body, sysLogConfig);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }
                    // OpenVPN Server Configuration
                    if (chkOpenVpnServer.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "OpenVPN Server Configuration", 1, mainPart);
                        ConfigDocumentGenerator.AddOpenVpnServerConfigToDocument(openVPNServerConfig, body, mainPart);

                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Other Configurations
                    if (chkOtherConfigs.IsChecked == true)
                    {
                        DocumentHelper.AddHeading(body, "Other Configurations", 1, mainPart);
                        ConfigDocumentGenerator.AddOtherConfigsToDocument(otherConfigs, body, mainPart);
                        DocumentHelper.InsertSectionBreak(body, SectionMarkValues.NextPage);
                    }

                    // Add Header and Footer
                    HeaderHelper.AddHeader(mainPart, $"Solvia - Firewall Documentation for {textboxCustomerName.Text}");
                    FooterHelper.AddFooter(mainPart, "Page ");
                    mainPart.Document.Save();
                }
                OpenFile(saveFileDialog.FileName);
            }
        }
        private void OpenFile(string filePath)
        {

            // Prüfe, ob das Dokument erfolgreich erstellt wurde und existiert
            if (File.Exists(filePath))
            {
                // Öffne das Dokument
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Diese Option erlaubt es, das Standardprogramm für die Dateitypzuordnung zu verwenden
                };
                Process.Start(psi);
            }
            else
            {
                Console.WriteLine("Das Dokument konnte nicht erstellt werden oder wurde nicht gefunden.");
            }
        }
    }
}
