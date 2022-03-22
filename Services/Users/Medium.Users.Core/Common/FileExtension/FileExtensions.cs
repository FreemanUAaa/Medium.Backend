using System.Collections.Generic;

namespace Medium.Users.Core.Common.FileExtension
{
    public static class FileExtensions
    {
        private static readonly List<string> userPhotoAllowedExtension = new List<string>() { 
            ".gif", ".png", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp" 
        };

        private static readonly List<string> bioPhotoAllowedExtension = new List<string>() {
            ".gif", ".png", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp"
        };

        public static bool IsValidUserPhotoExtension(string extension)
        {
            return userPhotoAllowedExtension.Contains(extension);
        }

        public static bool IsValidBioPhotoExtension(string extension)
        {
            return bioPhotoAllowedExtension.Contains(extension);
        }
    }
}
