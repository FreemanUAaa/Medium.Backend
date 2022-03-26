using Medium.Users.Application.Handlers.UserPhotos.Commands.CreateUserPhoto;
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

namespace Medium.Users.Tests.Tests.UserPhotos.Commands
{
    public class CreateUserPhotoCommandHandlerTests : BaseCommandTests<CreateUserPhotoCommandHandler>
    {
        [Fact]
        public async void CreateUserPhotoCommandHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            IFormFile formFile = FormFileFactory.Create(Photo.Bytes, "test-user-photo.jpg");
            CreateUserPhotoCommandHandler handler = new CreateUserPhotoCommandHandler(Database, FileManager, Logger);
            CreateUserPhotoCommand command = new CreateUserPhotoCommand()
            {
                Photo = formFile,
                UserId = user.Id,
            };


            Guid userPhotoId = await handler.Handle(command, CancellationToken.None);


            UserPhoto userPhoto = await Database.UserPhotos.FindAsync(userPhotoId);
            Assert.NotNull(userPhoto);
            Assert.True(File.Exists(Path.Combine(FileManager.UserSavePhotoPath, userPhoto.FileName)));
        }

        [Fact]
        public async void CreateUserPhotoCommandHandlerFailOnWrongFileExtension()
        {
            User user = await CreateAndAddUserToDatabase();
            IFormFile formFile = FormFileFactory.Create(Photo.Bytes, "test-user-photo.none");
            CreateUserPhotoCommandHandler handler = new CreateUserPhotoCommandHandler(Database, FileManager, Logger);
            CreateUserPhotoCommand command = new CreateUserPhotoCommand()
            {
                Photo = formFile,
                UserId = user.Id,
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void CreateUserPhotoCommandHandlerFailOnWrongUserId()
        {
            CreateUserPhotoCommandHandler handler = new CreateUserPhotoCommandHandler(Database, FileManager, Logger);
            CreateUserPhotoCommand command = new CreateUserPhotoCommand();


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
