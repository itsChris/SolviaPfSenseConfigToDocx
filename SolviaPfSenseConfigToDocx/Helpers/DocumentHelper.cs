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
            Paragraph paraTocTitle = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Left }));
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

        public static void AddHeadingByStyle(Body body, string text, string styleName)
        {
            // Create a paragraph for the heading
            Paragraph paraHeading = new Paragraph();

            // Create a run to hold the text
            Run runHeading = new Run();
            runHeading.Append(new Text(text));

            // Set the paragraph properties to apply the heading style by styleName
            ParagraphProperties paraProperties = new ParagraphProperties();
            paraProperties.Append(new ParagraphStyleId() { Val = styleName });

            // Add the paragraph properties and run to the paragraph
            paraHeading.Append(paraProperties);
            paraHeading.Append(runHeading);

            // Append the paragraph to the body
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

        public static void AddParagraph(Body body, string key, string value)
        {
            // Create a new paragraph
            Paragraph para = new Paragraph();

            // Create a run for the key (bold text)
            Run runKey = new Run();
            RunProperties runKeyProperties = new RunProperties();
            runKeyProperties.Append(new Bold());  // Make the key bold
            runKey.Append(runKeyProperties);
            runKey.Append(new Text(key + ": "));  // Add colon and space after the key

            // Create a run for the value (normal text)
            Run runValue = new Run();
            runValue.Append(new Text(value));

            // Append both runs (key and value) to the paragraph
            para.Append(runKey);
            para.Append(runValue);

            // Add the paragraph to the body
            body.Append(para);
        }

        public static void AddTableFromObject<T>(Body body, T obj)
        {
            // Check if the object is an IEnumerable but not a string
            if (obj is System.Collections.IEnumerable && obj is not string)
            {
                // Handle the case where obj is a collection of objects (e.g., List<Interface>)
                var list = obj as System.Collections.IEnumerable;

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        // Add a heading or some separator between items
                        AddHeadingByStyle(body, $"{item.GetType().Name} Details", "Heading 2");

                        // Render each item in the list
                        AddTableFromSingleObject(body, item);
                    }
                }
            }
            else
            {
                // Handle the case where obj is a single object
                AddTableFromSingleObject(body, obj);
            }
        }






        private static void AddTableFromSingleObject(Body body, object obj)
        {
            // Create a new table
            Table table = new Table();

            // Set table properties for borders and width
            TableProperties tableProperties = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                ),
                // Set table width to 100% of the page width
                new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct }
            );
            table.AppendChild(tableProperties);

            // Use reflection to get properties and their values
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Get the property name (Key)
                string propertyName = property.Name;

                // Check if the property is a List<string>
                if (typeof(IEnumerable<string>).IsAssignableFrom(property.PropertyType))
                {
                    var list = property.GetValue(obj) as IEnumerable<string>;

                    if (list != null)
                    {
                        // Create a row for the key
                        TableRow keyRow = new TableRow();
                        TableCell keyCell = new TableCell();
                        keyCell.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text(propertyName))));
                        keyRow.Append(keyCell);

                        // Add an empty value cell in the key row (span entire row)
                        TableCell emptyValueCell = new TableCell();
                        emptyValueCell.Append(new Paragraph()); // Always ensure the cell contains a paragraph
                        keyRow.Append(emptyValueCell);
                        table.Append(keyRow);

                        // Create a row for each item in the list
                        foreach (var item in list)
                        {
                            TableRow valueRow = new TableRow();

                            // Empty key cell for spacing
                            TableCell emptyKeyCell = new TableCell();
                            emptyKeyCell.Append(new Paragraph()); // Always ensure the cell contains a paragraph
                            valueRow.Append(emptyKeyCell);

                            // Value cell with the list item
                            TableCell valueCell = new TableCell();
                            valueCell.Append(new Paragraph(new Run(new Text(item))));
                            valueRow.Append(valueCell);

                            table.Append(valueRow);
                        }
                    }
                }
                else if (IsPrimitiveType(property.PropertyType))
                {
                    // Get the property value (Value)
                    var propertyValue = property.GetValue(obj)?.ToString() ?? "N/A";  // Handle null values

                    // Create a new table row
                    TableRow tableRow = new TableRow();

                    // Create a cell for the key (bold text)
                    TableCell keyCell = new TableCell();
                    keyCell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "2500" })); // Set width to 50% of the table width
                    Paragraph keyParagraph = new Paragraph(new Run(new RunProperties(new Bold()), new Text(propertyName)));
                    keyCell.Append(keyParagraph);
                    tableRow.Append(keyCell);

                    // Create a cell for the value (normal text)
                    TableCell valueCell = new TableCell();
                    valueCell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "2500" })); // Set width to 50% of the table width
                    Paragraph valueParagraph = new Paragraph(new Run(new Text(propertyValue)));
                    valueCell.Append(valueParagraph);
                    tableRow.Append(valueCell);

                    // Append the row to the table
                    table.Append(tableRow);
                }
            }

            // Append the table to the body
            body.Append(table);
        }

        private static bool IsPrimitiveType(Type type)
        {
            // Check if the type is a primitive, string, or other basic type
            return type.IsPrimitive ||
                   type.IsValueType ||
                   type == typeof(string) ||
                   type == typeof(DateTime) ||
                   type == typeof(decimal) ||
                   type == typeof(Guid);
        }


    }
}
