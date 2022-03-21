using MediatR;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ILogger<DeleteUserCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public DeleteUserCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<DeleteUserCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            UserPhoto userPhoto = await database.UserPhotos.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            List<BioPhoto> bioPhotos = await database.BioPhotos.Where(x => x.UserId == request.UserId).ToListAsync();

            DeleteUserPhoto(userPhoto);

            DeleteBioPhotos(bioPhotos);

            database.Users.Remove(user);
            await database.SaveChangesAsync(cancellationToken);


            logger.LogInformation($"The user with: \"{request.UserId}\" ID has been removed");

            return Unit.Value;
        }

        private void DeleteUserPhoto(UserPhoto userPhoto)
        {
            if (userPhoto != null)
            {
                fileManager.DeleteFileAsync(Path.Combine(fileManager.UserSavePhotoPath, userPhoto.FileName));
                database.UserPhotos.Remove(userPhoto);
            }
        }

        private void DeleteBioPhotos(List<BioPhoto> bioPhotos)
        {
            if (bioPhotos != null || bioPhotos.Count != 0)
            {
                foreach (BioPhoto photo in bioPhotos)
                {
                    fileManager.DeleteFileAsync(Path.Combine(fileManager.BioSavePhotoPath, photo.FileName));
                    database.BioPhotos.Remove(photo);
                }
            }
        }
    }
}
