using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Models
{
    public abstract class TemplateItem
    {
        public abstract void SetField(
            PdfStamper stamper, PdfReader reader, List<BaseFont> baseFonts);
    }
}
