using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Items
{
    public class ImageItem : TemplateItem
    {
        public string Key { get; set; } = "";
        public string FilePath { get; set; } = "";
        public ImageItem(string key, string filePath)
        {
            Key = key;
            FilePath = filePath;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            var image = Image.GetInstance(FilePath);
            var button = form.GetNewPushbuttonFromField(Key);
            button.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
            button.ProportionalIcon = true;
            button.Image = image;
            form.ReplacePushbuttonField(Key, button.Field);
        }
    }
}
