using System;

namespace Medium.Desings.Core.Models
{
    public class HeaderNameText
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string ColorRgb { get; set; }


        public Guid HeaderNameId { get; set; }

        public HeaderName HeaderName { get; set; }
    }
}
