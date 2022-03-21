using Microsoft.AspNetCore.Http;
using System.Threading;

namespace Medium.Users.Core.Interfaces.Services
{
    public interface IFileManager
    {
        string UserSavePhotoPath { get; set; }

        string BioSavePhotoPath { get; set; }

        void SaveFileAsync(IFormFile file, string path, CancellationToken cancellationToken = default);

        void DeleteFileAsync(string path, CancellationToken cancellationToken = default);
    }
}
