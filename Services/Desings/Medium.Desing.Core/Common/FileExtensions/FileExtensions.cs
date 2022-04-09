using System.Collections.Generic;

namespace Medium.Desings.Core.Common.FileExtensions
{
    public static class FileExtensions
    {
        private static readonly List<string> headerImageAllowedExtension = new List<string>() {
            ".gif", ".png", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp"
        };

        private static readonly List<string> logoAllowedExtension = new List<string>() {
            ".gif", ".png", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp"
        };

        public static bool IsValidHeaderImageExtension(string extension)
        {
            return headerImageAllowedExtension.Contains(extension);
        }

        public static bool IsValidLogoExtension(string extension)
        {
            return logoAllowedExtension.Contains(extension);
        }
    }
}
