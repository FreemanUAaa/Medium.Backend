using MediatR;
using Medium.Users.Core.Common.FileExtension;
using Medium.Users.Core.Common.FileName;
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

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.CreateUserPhoto
{
    public class CreateUserPhotoCommandHandler : IRequestHandler<CreateUserPhotoCommand, Guid>
    {
        private readonly ILogger<CreateUserPhotoCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public CreateUserPhotoCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<CreateUserPhotoCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Guid> Handle(CreateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            if (!database.Users.Any(x => x.Id == request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound); 
            }

            UserPhoto userPhoto = await database.UserPhotos.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            string fileExtension = Path.GetExtension(request.Photo.FileName);

            if (!FileExtensions.IsValidUserPhotoExtension(fileExtension))
            {
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            string newFilePath;
            Guid userPhotoId;

            if (userPhoto != null)
            {
                newFilePath = Path.Combine(fileManager.UserSavePhotoPath, userPhoto.FileName);
                userPhotoId = userPhoto.Id;
            }
            else
            {
                string newFileName = FileNameGenerator.GenerateUniqueFileName(fileManager.UserSavePhotoPath, fileExtension, 10);

                if (newFileName == null)
                {
                    throw new Exception(ExceptionStrings.FailedUploadPhoto);
                }

                newFilePath = Path.Combine(fileManager.UserSavePhotoPath, newFileName);

                UserPhoto newUserPhoto = new UserPhoto()
                {
                    Id = Guid.NewGuid(),
                    FileName = newFileName,
                    UserId = request.UserId,
                };

                database.UserPhotos.Add(newUserPhoto);
                await database.SaveChangesAsync(cancellationToken);

                userPhotoId = newUserPhoto.Id;
            }

            try
            {
                fileManager.SaveFileAsync(request.Photo, newFilePath);
            }
            catch
            {
                logger.LogError("There was an error saving the file");
                throw new Exception(ExceptionStrings.FailedUploadPhoto);
            }

            logger.LogInformation($"Photo saved successfully path: \"{newFilePath}\"");

            return userPhotoId;
        }
    }
}
