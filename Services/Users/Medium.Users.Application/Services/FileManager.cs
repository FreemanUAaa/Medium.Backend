using Medium.Users.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Services
{
    public class FileManager : IFileManager
    {
        public string UserSavePhotoPath { get; set; }
        public string BioSavePhotoPath { get; set; }

        public FileManager(string userSavePhotoPath, string bioSavePhotoPath) =>
            (UserSavePhotoPath, BioSavePhotoPath) = (userSavePhotoPath, bioSavePhotoPath);

        public async void DeleteFileAsync(string path, CancellationToken cancellationToken = default)
        {
            if (path == null || !File.Exists(path))
            {
                return;
            }

            await Task.Run(() => File.Delete(path));
        }

        public async void SaveFileAsync(IFormFile file, string path, CancellationToken cancellationToken = default)
        {
            if (file == null | path == null)
            {
                return;
            }

            using FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
