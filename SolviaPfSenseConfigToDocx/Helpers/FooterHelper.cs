using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public static class FooterHelper
{
    public static SectionProperties AddFooter(MainDocumentPart mainPart, string footerText = "Page ")
    {
        // Add new footer part to the document
        FooterPart footerPart = mainPart.AddNewPart<FooterPart>();
        Footer footer = new Footer();

        // Create a centered paragraph for the footer with page number field
        Paragraph footerParagraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
        Run footerRun = new Run(new Text(footerText));
        footerRun.Append(new SimpleField() { Instruction = "PAGE" });  // Page number
        footerParagraph.Append(footerRun);
        footer.Append(footerParagraph);
        footerPart.Footer = footer;

        // Create the section properties and assign the footer reference
        SectionProperties sectionProperties = new SectionProperties();

        // Reference for the footer part
        FooterReference footerReference = new FooterReference() { Type = HeaderFooterValues.Default, Id = mainPart.GetIdOfPart(footerPart) };
        sectionProperties.Append(footerReference);

        // Ensure different first page settings
        sectionProperties.AppendChild(new TitlePage());

        return sectionProperties;
    }
}
