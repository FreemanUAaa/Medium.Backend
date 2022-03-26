using Medium.Users.Application.Handlers.BioPhotos.Commands.CreateBioPhoto;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Assets;
using Medium.Users.Tests.Common;
using Medium.Users.Tests.Tests.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.BioPhotos.Commands
{
    public class CreateBioPhotoCommandHandlerTests : BaseCommandTests<CreateBioPhotoCommandHandler>
    {
        [Fact]
        public async void CreateBioPhotoCommandHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            IFormFile formFile = FormFileFactory.Create(Photo.Bytes, "test-bio-photo.jpg");
            CreateBioPhotoCommandHandler handler = new CreateBioPhotoCommandHandler(Database, FileManager, Logger);
            CreateBioPhotoCommand command = new CreateBioPhotoCommand()
            {
                Photo = formFile,
                UserId = user.Id,
            };


            Guid bioPhotoId = await handler.Handle(command, CancellationToken.None);


            BioPhoto bioPhoto = await Database.BioPhotos.FindAsync(bioPhotoId);
            Assert.NotNull(bioPhoto);
            Assert.True(File.Exists(Path.Combine(FileManager.BioSavePhotoPath, bioPhoto.FileName)));
        }

        [Fact]
        public async void CreateBioPhotoCommandHandlerFailOnWrongFileExtension()
        {

            User user = await CreateAndAddUserToDatabase();
            IFormFile formFile = FormFileFactory.Create(Photo.Bytes, "test-bio-photo.none");
            CreateBioPhotoCommandHandler handler = new CreateBioPhotoCommandHandler(Database, FileManager, Logger);
            CreateBioPhotoCommand command = new CreateBioPhotoCommand()
            {
                Photo = formFile,
                UserId = user.Id,
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void CreateBioPhotoCommandHandlerFailOnWrongUserId()
        {
            CreateBioPhotoCommandHandler handler = new CreateBioPhotoCommandHandler(Database, FileManager, Logger);
            CreateBioPhotoCommand command = new CreateBioPhotoCommand();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<User> CreateAndAddUserToDatabase()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "test-name",
                Email = "test-email",
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}
