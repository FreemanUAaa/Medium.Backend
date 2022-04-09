using System;

namespace Medium.Desings.Core.Models
{
    public class HeaderName
    {
        public Guid Id { get; set; }

        public bool IsTextSelected { get; set; }

        public HeaderNameLogo Logo { get; set; }

        public HeaderNameText Text { get; set; }


        public Guid HeaderId { get; set; }

        public Header Header { get; set; }
    }
}
