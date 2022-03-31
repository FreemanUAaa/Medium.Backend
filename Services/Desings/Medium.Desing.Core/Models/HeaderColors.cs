using System;

namespace Medium.Desings.Core.Models
{
    public class HeaderColors
    {
        public Guid Id { get; set; }

        public bool IsGradient { get; set; }

        public string ColorRgb { get; set; }


        public Guid HeaderId { get; set; }

        public Header Header { get; set; }
    }
}
