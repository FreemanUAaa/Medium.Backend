using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Desings.Core.Interfaces.Interfaces
{
    public interface IFileManager
    {
        string FontSavePath { get; set; }

        string HeaderSaveImagePath { get; set; }

        string HeaderSaveLogoPath { get; set; }

        Task SaveFileAsync(IFormFile file, string path, CancellationToken cancellationToken = default);

        Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);
    }
}
