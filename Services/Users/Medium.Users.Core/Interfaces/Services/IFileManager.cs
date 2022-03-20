using Microsoft.AspNetCore.Http;
using System.Threading;

namespace Medium.Users.Core.Interfaces.Services
{
    public interface IFileManager
    {
        string UserSavePhotoPath { get; set; }

        string BioSavePhotoPath { get; set; }

        void SavePhotoAsync(IFormFile file, string path, CancellationToken cancellationToken = default);

        void DeletePhotoAsync(string path, CancellationToken cancellationToken = default);
    }
}
