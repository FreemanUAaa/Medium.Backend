using System;

namespace Medium.Desings.Core.Models
{
    public class MainFonts
    {
        public Guid Id { get; set; }

        public Guid TitleFontId { get; set; }

        public Guid BodyFontId { get; set; }
        
        public Guid DetailsFontId { get; set; }


        public Guid DesingId { get; set; }

        public Desing Desing { get; set; }
    }
}
