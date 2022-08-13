using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Fields
{
    public class CheckBoxItem : TemplateItem
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public CheckBoxItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            form.SetField(Key, Value);
        }
    }
}
