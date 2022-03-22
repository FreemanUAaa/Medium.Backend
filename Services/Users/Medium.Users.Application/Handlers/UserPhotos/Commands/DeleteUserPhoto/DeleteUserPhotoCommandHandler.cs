using MediatR;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommandHandler : IRequestHandler<DeleteUserPhotoCommand>
    {
        private readonly ILogger<DeleteUserPhotoCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public DeleteUserPhotoCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<DeleteUserPhotoCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Unit> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            if (!database.Users.Any(x => x.Id == request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            UserPhoto userPhoto = await database.UserPhotos.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (userPhoto == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(fileManager.UserSavePhotoPath, userPhoto.FileName);

            if (File.Exists(filePath))
            {
                try
                {
                    fileManager.DeleteFileAsync(filePath);
                }
                catch
                {
                    throw new Exception(ExceptionStrings.FailedDeletePhoto);
                }
            }

            database.UserPhotos.Remove(userPhoto);
            await database.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
