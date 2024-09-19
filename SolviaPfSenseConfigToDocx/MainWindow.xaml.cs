using DocumentFormat.OpenXml.Packaging;
using Microsoft.Win32;
using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.DocumentGenerators;
using SolviaPfSenseConfigToDocx.Parsers;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Word = DocumentFormat.OpenXml.Wordprocessing;  // Alias for Open XML Wordprocessing

namespace SolviaPfSenseConfigToDocx
{
    public partial class MainWindow : Window
    {
        private PfSenseConfigParser pfSenseConfigParser;
        private SystemConfig systemConfig;
        private IpSecVPNConfig ipSecVpnConfig;


        public MainWindow()
        {
            InitializeComponent();

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
                var users = pfSenseConfigParser.ParseUsers();
                var groups = pfSenseConfigParser.ParseGroups();
                var interfaces = pfSenseConfigParser.ParseInterfaces();
                var staticRoutes = pfSenseConfigParser.ParseStaticRoutes();
                var dhcpConfig = pfSenseConfigParser.ParseDHCPConfig();
                var firewallConfig = pfSenseConfigParser.ParseFirewallRulesAndNAT();
                ipSecVpnConfig = pfSenseConfigParser.ParseIpSecVPNConfig();
                var certificatesAndCA = pfSenseConfigParser.ParseCertificatesAndCA();
                var otherConfigs = pfSenseConfigParser.ParseOtherConfigurations();

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

            string filePath = "PfSenseConfigSummary.docx";

            ConfigDocumentGenerator.GenerateDocument(systemConfig, ipSecVpnConfig, filePath);

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                // Add a main document part.
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure
                mainPart.Document = new Word.Document();
                Word.Body body = new Word.Body();

                // Iterate through parsed data and add it to the document
                foreach (var section in configData)
                {
                    // Add section title
                    Word.Paragraph heading = new Word.Paragraph(new Word.Run(new Word.Text($"Section: {section.Key}")));
                    heading.ParagraphProperties = new Word.ParagraphProperties(new Word.Justification() { Val = Word.JustificationValues.Center });
                    heading.ParagraphProperties.SpacingBetweenLines = new Word.SpacingBetweenLines() { After = "200" };
                    heading.ParagraphProperties.ParagraphStyleId = new Word.ParagraphStyleId() { Val = "Heading1" };
                    body.Append(heading);

                    // Add section entries
                    foreach (var entry in section.Value)
                    {
                        Word.Paragraph entryParagraph = new Word.Paragraph(new Word.Run(new Word.Text(entry)));
                        entryParagraph.ParagraphProperties = new Word.ParagraphProperties(new Word.SpacingBetweenLines() { After = "100" });
                        body.Append(entryParagraph);
                    }
                }
                // Save the changes to the document
                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
            MessageBox.Show($"Document saved as {filePath}", "Export Completed", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
