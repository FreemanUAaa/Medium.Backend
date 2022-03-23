using MediatR;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.BioPhotos.Queries.GetBioPhotoBytes
{
    public class GetBioPhotoBytesQueryHandler : IRequestHandler<GetBioPhotoBytesQuery, byte[]>
    {
        private readonly ILogger<GetBioPhotoBytesQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public GetBioPhotoBytesQueryHandler(IDatabaseContext database, IFileManager fileManager, ILogger<GetBioPhotoBytesQueryHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<byte[]> Handle(GetBioPhotoBytesQuery request, CancellationToken cancellationToken)
        {
            BioPhoto bioPhoto = await database.BioPhotos.FindAsync(request.BioPhotoId);

            if (bioPhoto == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(fileManager.BioSavePhotoPath, bioPhoto.FileName);

            if (!File.Exists(filePath))
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            byte[] bytes = await File.ReadAllBytesAsync(filePath);

            logger.LogInformation("User's photo was successfully received");

            return bytes;
        }
    }
}
