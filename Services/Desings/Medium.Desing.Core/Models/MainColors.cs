using System;

namespace Medium.Desings.Core.Models
{
    public class MainColors
    {
        public Guid Id { get; set; }

        public string AccentRgb { get; set; }

        public string BackgroundRgb { get; set; }


        public Guid DesingId { get; set; }

        public Desing Desing { get; set; }
    }
}
