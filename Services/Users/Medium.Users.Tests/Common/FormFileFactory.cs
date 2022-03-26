using Microsoft.AspNetCore.Http;
using System.IO;

namespace Medium.Users.Tests.Common
{
    public static class FormFileFactory
    {
        public static IFormFile Create(byte[] bytes, string fileName)
        {
            MemoryStream stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, fileName, fileName);
        }
    }
}
