using System;

namespace Medium.Desings.Core.Models
{
    public class Desing
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool ShowBlogroll { get; set; }

        public Header Header { get; set; }

        public MainColors Colors { get; set; }

        public MainFonts Fonts { get; set; }
    }
}
