using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;

namespace PdfTemplate.iTextSharp.LGPLv2
{
    public static class PdfTemplaterExt
    {
        public static void ToPdf(this PdfTemplater template, Stream output)
        {
            var doc = new Document();
            var copy = new PdfCopy(doc, output);
            doc.Open();
            
            foreach (var page in template.Pages)
            {
                var ms = new MemoryStream();
                var reader = new PdfReader(page.FilePath);
                var stamper = new PdfStamper(reader, ms);
                var baseFonts = page.BaseFonts.ToList();
                foreach (var baseFont in baseFonts)
                {
                    stamper.AcroFields.AddSubstitutionFont(baseFont);
                }
                foreach (var item in page.Items)
                {
                    item.SetField(stamper, reader, baseFonts);
                }
                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();

                ms.Position = 0;
                var copyReader = new PdfReader(ms);
                for (var i = 0; i < copyReader.NumberOfPages; i++)
                {
                    copy.AddPage(copy.GetImportedPage(copyReader, i + 1));
                }
                copyReader.Close();
            }
            doc.Close();
            copy.Close();
        }
    }
}