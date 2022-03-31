using System;

namespace Medium.Desings.Core.Models
{
    public class Header
    {
        public Guid Id { get; set; }

        public HeaderColors Colors { get; set; }

        public HeaderImage Image { get; set; }

        public HeaderName Name { get; set; }

        public bool ShowBlogroll { get; set; }


        public Guid DesingId { get; set; }

        public Desing Desing { get; set; }
    }
}
