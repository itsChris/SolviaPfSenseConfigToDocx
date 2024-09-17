using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SolviaPfSenseConfigToDocx.Helpers
{
    public static class HeaderHelper
    {
        public static void AddHeader(MainDocumentPart mainPart, string headerText)
        {
            HeaderPart headerPart = mainPart.AddNewPart<HeaderPart>();
            Header header = new Header();
            Paragraph headerParagraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            Run headerRun = new Run();
            headerRun.Append(new Text(headerText));
            headerParagraph.Append(headerRun);
            header.Append(headerParagraph);
            headerPart.Header = header;

            // Retrieve or create the SectionProperties
            SectionProperties sectionProperties = mainPart.Document.Body.Elements<SectionProperties>().FirstOrDefault() ?? new SectionProperties();
            HeaderReference headerReference = new HeaderReference() { Type = HeaderFooterValues.Default, Id = mainPart.GetIdOfPart(headerPart) };
            sectionProperties.Append(headerReference);

            // If the section properties are new, append them to the body
            if (!mainPart.Document.Body.Elements<SectionProperties>().Any())
            {
                mainPart.Document.Body.Append(sectionProperties);
            }
        }
    }
}
