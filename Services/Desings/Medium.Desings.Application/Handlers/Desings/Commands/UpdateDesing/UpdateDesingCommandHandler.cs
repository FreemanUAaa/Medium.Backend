using MediatR;
using Medium.Desings.Core.Common.FileExtensions;
using Medium.Desings.Core.Common.FileNames;
using Medium.Desings.Core.Exceptions;
using Medium.Desings.Core.Interfaces;
using Medium.Desings.Core.Interfaces.Interfaces;
using Medium.Desings.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing
{
    public class UpdateDesingCommandHandler : IRequestHandler<UpdateDesingCommand>
    {
        private readonly ILogger<UpdateDesingCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public UpdateDesingCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<UpdateDesingCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);

        public async Task<Unit> Handle(UpdateDesingCommand request, CancellationToken cancellationToken)
        {
            Desing desing = await database.Desings.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (desing == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            (desing.ShowBlogroll, desing.Header.Name.IsTextSelected) =
                (request.ShowBlogroll, request.IsTextSelected);

            if (request.Colors != null)
            {
                (desing.Colors.AccentRgb, desing.Colors.BackgroundRgb) = 
                    (request.Colors.AccentRgb, request.Colors.BackgraundRgb);
            }

            if (request.FontIds != null)
            {
                (desing.Fonts.BodyFontId, desing.Fonts.TitleFontId, desing.Fonts.DetailsFontId) =
                    (request.FontIds.Body, request.FontIds.Title, request.FontIds.Details);
            }

            if (request.HeaderColor != null)
            {
                (desing.Header.Colors.ColorRgb, desing.Header.Colors.IsGradient) = 
                    (request.HeaderColor.ColorRgb, request.HeaderColor.IsGradient);
            }

            if (request.HeaderImage != null)
            {
                desing.Header.Image = UpdateHeaderImage(desing.Header.Image, request.HeaderImage);
            }

            if (request.NameText != null)
            {
                (desing.Header.Name.Text.Text, desing.Header.Name.Text.ColorRgb) =
                    (request.NameText.Text, request.NameText.ColorRgb);
            }

            if (request.NameLogo != null)
            {
                desing.Header.Name.Logo = UpdateLogo(desing.Header.Name.Logo, request.NameLogo);
            }

            database.Desings.Update(desing);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Design updated successfully");

            return Unit.Value;
        }

        private HeaderImage UpdateHeaderImage(HeaderImage headerImage, HeaderImageRequest request)
        {
            string fileExtension = Path.GetExtension(request.Image.FileName);

            if (!FileExtensions.IsValidHeaderImageExtension(fileExtension))
            {
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            string newFileName = FileNameGenerator.GenerateUniqueFileName(fileManager.HeaderSaveImagePath, fileExtension, 10);

            if (newFileName == null)
            {
                throw new Exception(ExceptionStrings.FailedUploadPhoto);
            }

            try
            {
                string oldFilePath = Path.Combine(fileManager.HeaderSaveImagePath, headerImage.FileName);
                
                if (File.Exists(oldFilePath))
                {
                    fileManager.DeleteFileAsync(oldFilePath);
                }

                fileManager.SaveFileAsync(request.Image, Path.Combine(fileManager.HeaderSaveImagePath, newFileName));
            }
            catch
            {
                logger.LogWarning("There was an error saving the file");
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            (headerImage.Display, headerImage.Position, headerImage.FileName) =
                (request.Display, request.Position, newFileName);

            return headerImage;
        }

        private HeaderNameLogo UpdateLogo(HeaderNameLogo logo, IFormFile request)
        {
            string fileExtension = Path.GetExtension(request.FileName);

            if (!FileExtensions.IsValidHeaderImageExtension(fileExtension))
            {
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            string newFileName = FileNameGenerator.GenerateUniqueFileName(fileManager.HeaderSaveImagePath, fileExtension, 10);

            if (newFileName == null)
            {
                throw new Exception(ExceptionStrings.FailedUploadPhoto);
            }

            try
            {
                string oldFilePath = Path.Combine(fileManager.HeaderSaveImagePath, request.FileName);

                if (File.Exists(oldFilePath))
                {
                    fileManager.DeleteFileAsync(oldFilePath);
                }

                fileManager.SaveFileAsync(request, Path.Combine(fileManager.HeaderSaveImagePath, newFileName));
            }
            catch
            {
                logger.LogWarning("There was an error saving the file");
                throw new Exception(ExceptionStrings.FileExtensionNotSupported);
            }

            logo.FileName = newFileName;

            return logo;
        }
    }
}
