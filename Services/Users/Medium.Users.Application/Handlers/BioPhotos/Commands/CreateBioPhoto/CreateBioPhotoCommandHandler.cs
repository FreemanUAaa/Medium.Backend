using MediatR;
using Medium.Users.Core.Common.FileExtensions;
using Medium.Users.Core.Common.FileNames;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.CreateBioPhoto
{
    public class CreateBioPhotoCommandHandler : IRequestHandler<CreateBioPhotoCommand, Guid>
    {
        private readonly ILogger<CreateBioPhotoCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public CreateBioPhotoCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<CreateBioPhotoCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Guid> Handle(CreateBioPhotoCommand request, CancellationToken cancellationToken)
        {
            if (!database.Users.Any(x => x.Id == request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            string fileExtension = Path.GetExtension(request.Photo.FileName);

            if (!FileExtensions.IsValidBioPhotoExtension(fileExtension))
            {
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            string newFileName = FileNameGenerator.GenerateUniqueFileName(fileManager.UserSavePhotoPath, fileExtension, 10);

            if (newFileName == null)
            {
                throw new Exception(ExceptionStrings.FailedUploadPhoto);
            }

            string newFilePath = Path.Combine(fileManager.BioSavePhotoPath, newFileName);

            try
            {
                await fileManager.SaveFileAsync(request.Photo, newFilePath);
            }
            catch
            {
                logger.LogWarning("There was an error saving the file");
                throw new Exception(ExceptionStrings.FailedUploadPhoto);
            }

            BioPhoto bioPhoto = new BioPhoto()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                FileName = newFileName,
            };

            database.BioPhotos.Add(bioPhoto);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation($"Photo saved successfully path: \"{newFilePath}\"");

            return bioPhoto.Id;
        }
    }
}
