using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.DocumentGenerators;
using SolviaPfSenseConfigToDocx.Parsers;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SolviaPfSenseConfigToDocx
{
    public partial class MainWindow : Window
    {
        private PfSenseConfigParser pfSenseConfigParser;
        private SystemConfig systemConfig;
        private IpSecVPNConfig ipSecVpnConfig;
        private List<Interface> interfaces;
        private List<StaticRoute> staticRoutes;
        private DHCPConfig dhcpConfig;
        private FirewallConfig firewallConfig;
        private CertificateConfig certificatesAndCA;
        private List<User> users;
        private List<Group> groups;
        private OtherConfigurations otherConfigs;

        public MainWindow()
        {
            InitializeComponent();



            // TODO
            Services.WordDocumentService wordDocumentService = new Services.WordDocumentService();
            wordDocumentService.CreateWordDocument("SampleDocument.docx");
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                pfSenseConfigParser = new PfSenseConfigParser(openFileDialog.FileName);
                
                // Parse each section

                systemConfig = pfSenseConfigParser.ParseSystemConfig();
                users = pfSenseConfigParser.ParseUsers();
                groups = pfSenseConfigParser.ParseGroups();
                interfaces = pfSenseConfigParser.ParseInterfaces();
                staticRoutes = pfSenseConfigParser.ParseStaticRoutes();
                dhcpConfig = pfSenseConfigParser.ParseDHCPConfig();
                firewallConfig = pfSenseConfigParser.ParseFirewallRulesAndNAT();
                ipSecVpnConfig = pfSenseConfigParser.ParseIpSecVPNConfig();
                certificatesAndCA = pfSenseConfigParser.ParseCertificatesAndCA();
                otherConfigs = pfSenseConfigParser.ParseOtherConfigurations();

                OutputRichTextBox.Document.Blocks.Clear(); // Clear previous content

                // Add SystemConfig data
                AddTextToRichTextBox($"Hostname: {systemConfig.Hostname}", Brushes.Blue, 16, FontWeights.Bold);
                AddTextToRichTextBox($"Domain: {systemConfig.Domain}", Brushes.Black, 14, FontWeights.Normal);
                AddTextToRichTextBox($"Next UID: {systemConfig.NextUID}", Brushes.Black, 14, FontWeights.Normal);
                AddTextToRichTextBox($"Next GID: {systemConfig.NextGID}", Brushes.Black, 14, FontWeights.Normal);

                // Add other sections in similar fashion...

                AddTextToRichTextBox("XML Parsing Completed.", Brushes.Green, 14, FontWeights.Bold);
            }
        }


        private void AddTextToRichTextBox(string text, Brush color, double fontSize, FontWeight fontWeight)
        {
            TextRange textRange = new TextRange(OutputRichTextBox.Document.ContentEnd, OutputRichTextBox.Document.ContentEnd)
            {
                Text = text + Environment.NewLine
            };
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, color);
            textRange.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            textRange.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                SearchPanel.Visibility = Visibility.Visible;
                SearchTextBox.Focus(); // Focus the search box
            }
        }

        // Search when pressing Enter in search box
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(sender, e); // Trigger search
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;

            if (!string.IsNullOrEmpty(searchText))
            {
                // Clear previous highlights
                ClearTextHighlights();

                // Search and highlight matches
                TextPointer pointer = OutputRichTextBox.Document.ContentStart;
                while (pointer != null)
                {
                    // Find the text in the document
                    TextRange searchRange = FindTextInRichTextBox(pointer, searchText);
                    if (searchRange != null)
                    {
                        searchRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow); // Highlight in yellow
                        pointer = searchRange.End; // Move to next match
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private TextRange FindTextInRichTextBox(TextPointer startPointer, string searchText)
        {
            while (startPointer != null)
            {
                if (startPointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = startPointer.GetTextInRun(LogicalDirection.Forward);
                    int index = textRun.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        TextPointer start = startPointer.GetPositionAtOffset(index);
                        TextPointer end = start.GetPositionAtOffset(searchText.Length);
                        return new TextRange(start, end);
                    }
                }
                startPointer = startPointer.GetNextContextPosition(LogicalDirection.Forward);
            }
            return null;
        }

        private void ClearTextHighlights()
        {
            TextRange documentRange = new TextRange(OutputRichTextBox.Document.ContentStart, OutputRichTextBox.Document.ContentEnd);
            documentRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent); // Clear highlights
        }

        private void ExportToDocxButton_Click(object sender, RoutedEventArgs e)
        {

            if (pfSenseConfigParser == null)
            {
                MessageBox.Show("Please select a file first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Use the new ParseConfig method
            Dictionary<string, List<string>> configData = pfSenseConfigParser.ParseConfig();

            string filePath = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_PfSenseConfigSummary.docx";

            ConfigDocumentGenerator.GenerateDocument(systemConfig, ipSecVpnConfig, filePath);

            var docxPath = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_SystemConfig.docx";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(docxPath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                if (chkSystemConfig.IsChecked == true)
                    ConfigDocumentGenerator.AddSystemConfigToDocument(systemConfig, body, mainPart);


                if (chkIpSecVpnConfig.IsChecked == true)
                    ConfigDocumentGenerator.AddIpSecVpnConfigToDocument(ipSecVpnConfig, body, mainPart);

                if (chkInterfaces.IsChecked == true)
                    ConfigDocumentGenerator.AddInterfaceConfigToDocument(interfaces, body, mainPart);
                if (chkStaticRoutes.IsChecked == true)

                if (chkDHCPConfig.IsChecked == true)

                if (chkFirewallConfig.IsChecked == true)

                if (chkCertificatesAndCA.IsChecked == true)

                if (chkUsers.IsChecked == true)

                if (chkGroups.IsChecked == true)

                if (chkOtherConfigs.IsChecked == true)
                
                mainPart.Document.Save();
            }
            OpenFile(filePath);
            OpenFile(docxPath);

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
