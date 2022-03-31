using System;

namespace Medium.Desings.Core.Models
{
    public class HeaderImage
    {
        public Guid Id { get; set; }

        public string Display { get; set; }

        public string FileName { get; set; }

        public string Position { get; set; }


        public Guid HeaderId { get; set; }

        public Header Header { get; set; }
    }
}
