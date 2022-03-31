using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing
{
    public class UpdateDesingCommand : IRequest
    {
        public Guid UserId { get; set; }

        public bool ShowBlogroll { get; set; }

        public Colors Colors { get; set; }

        public FontIds FontIds { get; set; }

        public NameText NameText { get; set; }

        public IFormFile NameLogo { get; set; }

        public HeaderColor HeaderColor { get; set; }

        public HeaderImage HeaderImage { get; set; }
    }

    public class HeaderColor
    {
        public bool IsGradient { get; set; }

        public string ColorRgb { get; set; }
    }

    public class NameText
    {
        public string Text { get; set; }

        public string ColorRgb { get; set; }
    }

    public class Colors
    {
        public string AccentRgb { get; set; }

        public string BackgraundRgb { get; set; }
    }

    public class FontIds
    {
        public Guid Title { get; set; }

        public Guid Body { get; set; }

        public Guid Details { get; set; }
    }

    public class HeaderImage
    {
        public string Display { get; set; }

        public IFormFile Image { get; set; }

        public string Position { get; set; }
    }
}
