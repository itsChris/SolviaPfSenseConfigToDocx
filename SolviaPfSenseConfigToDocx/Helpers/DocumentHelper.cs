using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace SolviaPfSenseConfigToDocx.Helpers
{
    public static class DocumentHelper
    {
        public static void AddTitlePage(Body body, string title)
        {
            Paragraph paraTitle = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            Run runTitle = new Run();
            runTitle.Append(new Text(title));
            RunProperties runPropertiesTitle = new RunProperties(new Bold(), new FontSize() { Val = "48" });
            runTitle.PrependChild(runPropertiesTitle);

            paraTitle.Append(runTitle);
            body.Append(paraTitle);
            body.Append(new Paragraph()); // Add blank line
        }

        public static void AddTableOfContents(Body body, MainDocumentPart mainPart)
        {
            // Add TOC Title
            Paragraph paraTocTitle = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            Run runTocTitle = new Run();
            runTocTitle.Append(new Text("Table of Contents"));
            RunProperties runPropertiesTocTitle = new RunProperties(new Bold(), new FontSize() { Val = "36" });
            runTocTitle.PrependChild(runPropertiesTocTitle);
            paraTocTitle.Append(runTocTitle);
            body.Append(paraTocTitle);
            body.Append(new Paragraph()); // Add blank line

            // Add TOC field code
            Paragraph para = new Paragraph();
            Run runTOC = new Run();
            FieldChar begin = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            FieldCode fieldCode = new FieldCode() { Space = SpaceProcessingModeValues.Preserve };
            fieldCode.Text = " TOC \\o \"1-3\" \\h \\z \\u ";  // TOC field options for levels 1-3, hyperlinks, etc.
            FieldChar separate = new FieldChar() { FieldCharType = FieldCharValues.Separate };
            FieldChar end = new FieldChar() { FieldCharType = FieldCharValues.End };

            runTOC.Append(begin);
            runTOC.Append(fieldCode);
            runTOC.Append(separate);
            runTOC.Append(new Text("Table of Contents will be displayed here upon update"));
            runTOC.Append(end);

            para.Append(runTOC);
            body.Append(para);
        }

        public static void AddHeading(Body body, string text, int level, MainDocumentPart mainPart)
        {
            Paragraph paraHeading = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Left }));
            Run runHeading = new Run();
            runHeading.Append(new Text(text));

            // Run properties based on heading level
            RunProperties runPropertiesHeading = new RunProperties();
            ParagraphProperties paraProperties = new ParagraphProperties();

            if (level == 1)
            {
                runPropertiesHeading.Append(new Bold(), new FontSize() { Val = "32" });
                paraProperties.Append(new OutlineLevel() { Val = 0 });
            }
            else if (level == 2)
            {
                runPropertiesHeading.Append(new Italic(), new FontSize() { Val = "28" });
                paraProperties.Append(new OutlineLevel() { Val = 1 });
            }
            else
            {
                runPropertiesHeading.Append(new Underline() { Val = UnderlineValues.Single }, new FontSize() { Val = "24" });
                paraProperties.Append(new OutlineLevel() { Val = 2 });
            }

            runHeading.PrependChild(runPropertiesHeading);
            paraHeading.Append(paraProperties);
            paraHeading.Append(runHeading);
            body.Append(paraHeading);
        }



        public static void AddParagraph(Body body, string text)
        {
            Paragraph para = new Paragraph();
            Run run = new Run();
            run.Append(new Text(text));
            para.Append(run);
            body.Append(para);
        }
    }
}
