using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Drawing;

namespace PdfTemplate.iTextSharp.LGPLv2.Fields
{
    public class TextItem : TemplateItem
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public BaseFont? BaseFont { get; set; }
        public float? TextSize { get; set; }
        public Color? Color { get; set; }
        public Color? BgColor { get; set; }
        public TextItem(string key, string value, float? textSize = null, 
            Color? color = null, Color? bgColor = null, BaseFont? baseFont = null)
        {
            Key = key;
            Value = value;
            BaseFont = baseFont;
            TextSize = textSize;
            Color = color;
            BgColor = bgColor;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            if (BaseFont != null)
            {
                form.SetFieldProperty(Key, "textfont", BaseFont, null);
            }
            if (TextSize != null)
            {
                form.SetFieldProperty(Key, "textsize", TextSize, null);
            }
            if (Color != null)
            {
                form.SetFieldProperty(Key, "textcolor",
                    new BaseColor(Color.Value), null);
            }
            if (BgColor != null)
            {
                form.SetFieldProperty(Key, "bgcolor", 
                    new BaseColor(BgColor.Value), null);
            }
            form.SetField(Key, Value);
        }
    }
}
