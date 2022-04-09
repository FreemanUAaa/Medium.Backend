using System.IO;

namespace Medium.Desings.Core.Common.FileNames
{
    public static class FileNameGenerator
    {
        public static string GenerateUniqueFileName(string path, string fileExtension, int attempts)
        {
            string newFileName;
            string newFilePath;

            for (int i = 0; i <= attempts; i++)
            {
                newFileName = GetRandomFileName(fileExtension);

                newFilePath = Path.Combine(path, newFileName);

                if (!File.Exists(newFilePath))
                {
                    return newFileName;
                }
            }

            return null;
        }

        public static string GetRandomFileName(string fileExtension)
        {
            if (fileExtension.Contains("."))
            {
                return Path.GetRandomFileName() + fileExtension;
            }

            return Path.GetRandomFileName() + "." + fileExtension;
        }
    }
}
