using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SolviaPfSenseConfigToDocx.Helpers
{
    public static class FooterHelper
    {
        public static void AddFooter(MainDocumentPart mainPart, string footerText = "Page ")
        {
            FooterPart footerPart = mainPart.AddNewPart<FooterPart>();
            Footer footer = new Footer();
            Paragraph footerParagraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            Run footerRun = new Run();
            footerRun.Append(new Text(footerText));
            footerRun.Append(new SimpleField() { Instruction = "PAGE" });
            footerParagraph.Append(footerRun);
            footer.Append(footerParagraph);
            footerPart.Footer = footer;

            // Retrieve or create the SectionProperties
            SectionProperties sectionProperties = mainPart.Document.Body.Elements<SectionProperties>().FirstOrDefault() ?? new SectionProperties();
            FooterReference footerReference = new FooterReference() { Type = HeaderFooterValues.Default, Id = mainPart.GetIdOfPart(footerPart) };
            sectionProperties.Append(footerReference);

            // If the section properties are new, append them to the body
            if (!mainPart.Document.Body.Elements<SectionProperties>().Any())
            {
                mainPart.Document.Body.Append(sectionProperties);
            }
        }
    }
}
