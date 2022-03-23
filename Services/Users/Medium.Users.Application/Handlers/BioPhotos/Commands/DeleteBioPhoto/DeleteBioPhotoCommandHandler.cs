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

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.DeleteBioPhoto
{
    public class DeleteBioPhotoCommandHandler : IRequestHandler<DeleteBioPhotoCommand>
    {
        private readonly ILogger<DeleteBioPhotoCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public DeleteBioPhotoCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<DeleteBioPhotoCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Unit> Handle(DeleteBioPhotoCommand request, CancellationToken cancellationToken)
        {
            BioPhoto bioPhoto = await database.BioPhotos.FindAsync(request.BioPhotoId);

            if (bioPhoto == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(fileManager.BioSavePhotoPath, bioPhoto.FileName);

            if (File.Exists(filePath))
            {
                try
                {
                    fileManager.DeleteFileAsync(filePath);
                }
                catch
                {
                    logger.LogWarning("There was an error saving the file");
                    throw new Exception(ExceptionStrings.FailedDeletePhoto);
                }
            }

            database.BioPhotos.Remove(bioPhoto);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The file was successfully deleted");

            return Unit.Value;
        }
    }
}
