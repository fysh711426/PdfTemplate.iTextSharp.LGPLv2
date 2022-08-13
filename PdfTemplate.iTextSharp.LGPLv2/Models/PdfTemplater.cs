using System.Collections.Generic;

namespace PdfTemplate.iTextSharp.LGPLv2.Models
{
    public class PdfTemplater
    {
        public IEnumerable<TemplatePage> Pages { get; set; } = new List<TemplatePage>();
    }
}
