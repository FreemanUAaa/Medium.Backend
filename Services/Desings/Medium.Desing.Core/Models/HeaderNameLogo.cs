using System;

namespace Medium.Desings.Core.Models
{
    public class HeaderNameLogo
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }


        public Guid HeaderNameId { get; set; }

        public HeaderName HeaderName { get; set; }
    }
}
