using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public static class HeaderHelper
{
    public static SectionProperties AddHeader(MainDocumentPart mainPart, string headerText)
    {
        // Add new header part to the document
        HeaderPart headerPart = mainPart.AddNewPart<HeaderPart>();
        Header header = new Header();

        // Create a centered paragraph for the header text
        Paragraph headerParagraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
        Run headerRun = new Run(new Text(headerText));
        headerParagraph.Append(headerRun);
        header.Append(headerParagraph);
        headerPart.Header = header;

        // Create the section properties and assign the header reference
        SectionProperties sectionProperties = new SectionProperties();

        // Reference for the header part
        HeaderReference headerReference = new HeaderReference() { Type = HeaderFooterValues.Default, Id = mainPart.GetIdOfPart(headerPart) };
        sectionProperties.Append(headerReference);

        // Ensure different first page settings
        sectionProperties.AppendChild(new TitlePage());

        return sectionProperties;
    }
}
