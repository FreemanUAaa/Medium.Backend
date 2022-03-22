using MediatR;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.UserPhotos.Queries.GetUserPhotoBytes
{
    public class GetUserPhotoBytesQueryHandler : IRequestHandler<GetUserPhotoBytesQuery, byte[]>
    {
        private readonly ILogger<GetUserPhotoBytesQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public GetUserPhotoBytesQueryHandler(IDatabaseContext database, IFileManager fileManager, ILogger<GetUserPhotoBytesQueryHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<byte[]> Handle(GetUserPhotoBytesQuery request, CancellationToken cancellationToken)
        {
            UserPhoto userPhoto = await database.UserPhotos.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (userPhoto == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string userPhotoPath = Path.Combine(fileManager.UserSavePhotoPath, userPhoto.FileName);

            if (!File.Exists(userPhotoPath))
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            byte[] bytes = await File.ReadAllBytesAsync(userPhotoPath);

            logger.LogInformation("User's photo was successfully received");

            return bytes;
        }
    }
}
