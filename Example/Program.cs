using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfTemplate.iTextSharp.LGPLv2;
using PdfTemplate.iTextSharp.LGPLv2.Fields;
using PdfTemplate.iTextSharp.LGPLv2.Models;
using PdfTemplate.iTextSharp.LGPLv2.Zip;
using System.IO.Compression;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Pdf();
            Zip();
        }

        public static PdfTemplater GetTemplater()
        {
            var fontPaths = new List<string>
            {
                Path.Combine(_projectPath, @"Fonts\arial.ttf"),
                Path.Combine(_projectPath, @"Fonts\cwTeXHei-zhonly.ttf")
            };

            var template1 = Path.Combine(_projectPath, @"Templates\demo1.pdf");
            var template2 = Path.Combine(_projectPath, @"Templates\demo2.pdf");
            var imagePath = Path.Combine(_projectPath, @"Images\image.png");

            var templater = new PdfTemplater
            {
                Pages = new List<TemplatePage>
                {
                    new TemplatePage(fontPaths)
                    {
                        FilePath = template1,
                        Items = new List<TemplateItem>
                        {
                            new TextItem("Text", "Text"),
                            new TextItem("TextColor", "TextColor",
                                color: System.Drawing.Color.FromArgb(255, 106, 0),
                                textSize: 12f),
                            new ParagraphItem("Paragraph", (baseFonts) =>
                            {
                                var selector1 = new FontSelector();
                                foreach(var baseFont in baseFonts)
                                    selector1.AddFont(new Font(baseFont, 15f, Font.NORMAL));

                                var selector2 = new FontSelector();
                                foreach(var baseFont in baseFonts)
                                    selector2.AddFont(new Font(baseFont, 15f, Font.STRIKETHRU | Font.BOLD));

                                var paragraph = new Paragraph();
                                paragraph.Add(selector1.Process("abc "));
                                paragraph.Add(selector2.Process("中文測試"));
                                paragraph.Leading = 13;
                                return paragraph;
                            }),
                            new RadioButtonItem("RadioButton", "Yes"),
                            new CheckBoxItem("CheckBox2", "Yes"),
                            new CheckBoxItem("CheckBox3", "Yes"),
                            new ImageItem("Image", imagePath),
                            new BarcodeItem("Barcode", "123456789"),
                            new WaterMarkField("Text", "WaterMark",
                                fontPaths[0], 130f, 0.3f, 30f, -40f, 45)
                        }
                    },
                    new TemplatePage(fontPaths)
                    {
                        FilePath = template2,
                        Items = new List<TemplateItem>
                        {
                            new TextItem("Text", "Text"),
                            new TextItem("TextColor", "TextColor",
                                color: System.Drawing.Color.FromArgb(255, 106, 0),
                                textSize: 12f),
                            new ParagraphItem("Paragraph", (baseFonts) =>
                            {
                                var selector1 = new FontSelector();
                                foreach(var baseFont in baseFonts)
                                    selector1.AddFont(new Font(baseFont, 15f, Font.NORMAL));

                                var selector2 = new FontSelector();
                                foreach(var baseFont in baseFonts)
                                    selector2.AddFont(new Font(baseFont, 15f, Font.STRIKETHRU | Font.BOLD));

                                var paragraph = new Paragraph();
                                paragraph.Add(selector1.Process("abc "));
                                paragraph.Add(selector2.Process("中文測試"));
                                paragraph.Leading = 13;
                                return paragraph;
                            }),
                            new RadioButtonItem("RadioButton", "Yes"),
                            new CheckBoxItem("CheckBox2", "Yes"),
                            new CheckBoxItem("CheckBox3", "Yes"),
                            new ImageItem("Image", imagePath),
                            new BarcodeItem("Barcode", "123456789"),
                            new WaterMarkField("Text", "草稿",
                                fontPaths[1], 250f, 0.2f, 60f, -60f, 45)
                        }
                    }
                }
            };
            return templater;
        }

        public static void Pdf()
        {
            var output = Path.Combine(_projectPath, @"output.pdf");

            using (var fs = new FileStream(output,
                FileMode.Create, FileAccess.ReadWrite))
            {
                GetTemplater().ToPdf(fs);
            }
        }

        public static void Zip()
        {
            var output = Path.Combine(_projectPath, @"output.zip");

            using (var fs = new FileStream(output,
                FileMode.Create, FileAccess.ReadWrite))
            {
                using (var archive = new ZipArchive(
                    new ZipWrapStream(fs), ZipArchiveMode.Create))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        var zipEntry = archive.CreateEntry(
                            $"output{i}.pdf", CompressionLevel.Fastest);
                        using (var zipStream = zipEntry.Open())
                        {
                            GetTemplater().ToPdf(zipStream);
                        }
                    }
                }
            }
        }

        private static string _executingPath =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        private static string _projectPath =>
            Regex.Match(_executingPath, @"(.*?)\\bin").Groups[1].Value;
    }
}