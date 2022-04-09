using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing
{
    public class UpdateDesingCommand : IRequest
    {
        public Guid UserId { get; set; }

        public bool ShowBlogroll { get; set; }

        public bool IsTextSelected { get; set; }

        public ColorsRequest Colors { get; set; }

        public FontIdsRequest FontIds { get; set; }

        public NameTextRequest NameText { get; set; }

        public IFormFile NameLogo { get; set; }

        public HeaderColorRequest HeaderColor { get; set; }

        public HeaderImageRequest HeaderImage { get; set; }
    }

    public class HeaderColorRequest
    {
        public bool IsGradient { get; set; }

        public string ColorRgb { get; set; }
    }

    public class NameTextRequest
    {
        public string Text { get; set; }

        public string ColorRgb { get; set; }
    }

    public class ColorsRequest
    {
        public string AccentRgb { get; set; }

        public string BackgraundRgb { get; set; }
    }

    public class FontIdsRequest
    {
        public Guid Title { get; set; }

        public Guid Body { get; set; }

        public Guid Details { get; set; }
    }

    public class HeaderImageRequest
    {
        public string Display { get; set; }

        public IFormFile Image { get; set; }

        public string Position { get; set; }
    }
}
