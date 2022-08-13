using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;

namespace PdfTemplate.iTextSharp.LGPLv2.Models
{
    public class TemplatePage
    {
        public string FilePath { get; set; } = "";
        public IEnumerable<BaseFont> BaseFonts { get; set; } = new List<BaseFont>();
        public IEnumerable<TemplateItem> Items { get; set; } = new List<TemplateItem>();
        public TemplatePage()
        {
        }
        public TemplatePage(IEnumerable<BaseFont> baseFonts)
        {
            BaseFonts = baseFonts;
        }
        public TemplatePage(IEnumerable<string> fontPaths)
        {
            BaseFonts = fontPaths
                .Select(it => BaseFont.CreateFont(it,
                    BaseFont.IDENTITY_H, BaseFont.EMBEDDED)).ToList();
        }
    }
}
