using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Items
{
    public class WaterMarkField : TemplateItem
    {
        public string Key { get; set; } = "";
        public string Text { get; set; } = "";
        public BaseFont BaseFont { get; set; }
        public float FontSize { get; set; }
        public float Opacity { get; set; }
        /// <summary>
        /// Position offset x from center.
        /// </summary>
        public float PositionX { get; set; }
        /// <summary>
        /// Position offset y from center.
        /// </summary>
        public float PositionY { get; set; }
        public int Rotation { get; set; }
        public WaterMarkField(string key, string text, string fontPath, float fontSize,
            float opacity, float positionX, float positionY, int rotation)
            : this(key, text, BaseFont.CreateFont(fontPath,
                BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 
                    fontSize, opacity, positionX, positionY, rotation)
        {
        }
        public WaterMarkField(string key, string text, BaseFont baseFont, float fontSize, 
            float opacity, float positionX, float positionY, int rotation)
        {
            Key = key;
            Text = text;
            BaseFont = baseFont;
            FontSize = fontSize;
            Opacity = opacity;
            PositionX = positionX;
            PositionY = positionY;
            Rotation = rotation;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            var fieldPositions = form.GetFieldPositions(Key);
            var pdfContentByte = stamper.GetUnderContent((int)fieldPositions[0]);
            pdfContentByte.BeginText();
            pdfContentByte.SetFontAndSize(BaseFont, FontSize);
            pdfContentByte.SetRgbColorFill(210, 210, 210);
            pdfContentByte.SetGState(new PdfGState
            {
                FillOpacity = Opacity,
                StrokeOpacity = Opacity
            });
            var rect = reader.GetPageSizeWithRotation((int)fieldPositions[0]);
            var x = rect.Width / 2;
            var y = rect.Height / 2;
            pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER,
                Text, x + PositionX, y + PositionY, Rotation);
            pdfContentByte.EndText();
        }
    }
}
