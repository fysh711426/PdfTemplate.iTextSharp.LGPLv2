using PdfTemplate.iTextSharp.LGPLv2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Items
{
    public class BarcodeItem : TemplateItem
    {
        public string Key { get; set; } = "";
        public string Code { get; set; } = "";
        /// <summary>
        /// Show the start and stop character '*' in the text.
        /// </summary>
        public bool StartStopText { get; set; }
        public bool IsShowText { get; set; }
        public BarcodeItem(string key, string code, 
            bool startStopText = false, bool isShowText = true)
        {
            Key = key;
            Code = code;
            StartStopText = startStopText;
            IsShowText = isShowText;
        }
        public override void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts)
        {
            var form = stamper.AcroFields;
            var fieldPosition = form.GetFieldPositions(Key);
            var pdfContentByte = stamper.GetOverContent((int)fieldPosition[0]);
            var barcode = new Barcode39();
            barcode.Code = Code;
            if (!IsShowText)
                barcode.AltText = "";
            barcode.StartStopText = StartStopText;
            barcode.X = 0.8f;
            barcode.InkSpreading = 0f;
            barcode.N = 2f;
            barcode.Size = 10f;
            barcode.Baseline = 10f;
            barcode.BarHeight = 20f;
            barcode.GenerateChecksum = false;
            barcode.ChecksumText = false;
            var image = barcode.CreateImageWithBarcode(pdfContentByte, BaseColor.Black, BaseColor.Black);
            image.SetAbsolutePosition(fieldPosition[1], fieldPosition[2]);
            pdfContentByte.AddImage(image);
        }
    }
}
