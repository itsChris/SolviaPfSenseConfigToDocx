using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using SolviaPfSenseConfigToDocx.Helpers;

namespace SolviaPfSenseConfigToDocx.Services
{
    public class WordDocumentService
    {
        public void CreateWordDocument(string filepath)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                // Add the main document part
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Add Title Page
                DocumentHelper.AddTitlePage(body, "Open XML SDK Word Document with TOC");

                // Add the Table of Contents (ToC)
                DocumentHelper.AddTableOfContents(body, mainPart);

                // Add Content - Headings and Paragraphs
                DocumentHelper.AddHeading(body, "Heading 1 - Introduction", 1, mainPart);
                DocumentHelper.AddParagraph(body, "This is a normal paragraph in the Introduction section.");

                DocumentHelper.AddHeading(body, "Heading 2 - Details", 2, mainPart);
                DocumentHelper.AddParagraph(body, "This is a normal paragraph in the Details section.");

                DocumentHelper.AddHeading(body, "Heading 3 - Conclusion", 3, mainPart);
                DocumentHelper.AddParagraph(body, "This is a normal paragraph in the Conclusion section.");

                // Add Header and Footer
                HeaderHelper.AddHeader(mainPart, "My Document Header");
                FooterHelper.AddFooter(mainPart, "Page ");

                // Save the document
                mainPart.Document.Save();
            }
        }
    }
}