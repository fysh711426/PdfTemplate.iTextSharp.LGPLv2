using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Items
{
    public class ParagraphItem : TemplateItem
    {
        public string Key { get; set; } = "";
        public Func<List<BaseFont>, Paragraph> Handler { get; set; }
        public ParagraphItem(string key, Func<List<BaseFont>, Paragraph> handler)
        {
            Key = key;
            Handler = handler;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            var fieldPositions = form.GetFieldPositions(Key);
            var paragraph = Handler(baseFonts);
            var pdfContentByte = stamper.GetOverContent((int)fieldPositions[0]);
            var columnText = new ColumnText(pdfContentByte);
            columnText.AddElement(paragraph);
            columnText.SetSimpleColumn(fieldPositions[1], fieldPositions[2], fieldPositions[3], fieldPositions[4]);
            columnText.Go();
        }
    }
}
