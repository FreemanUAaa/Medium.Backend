using Medium.Users.Application.Handlers.UserPhotos.Commands.DeleteUserPhoto;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Assets;
using Medium.Users.Tests.Tests.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.UserPhotos.Commands
{
    public class DeleteUserPhotoCommandHandlerTests : BaseCommandTests<DeleteUserPhotoCommandHandler>
    {
        [Fact]
        public async void DeleteUserPhotoCommandHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            UserPhoto userPhoto = await CreateAndAddUserPhotoToDatabase(user.Id);
            DeleteUserPhotoCommandHandler handler = new DeleteUserPhotoCommandHandler(Database, FileManager, Cache, Logger);
            DeleteUserPhotoCommand command = new DeleteUserPhotoCommand() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.UserPhotos.FindAsync(userPhoto.Id));
            Assert.False(File.Exists(Path.Combine(FileManager.UserSavePhotoPath, userPhoto.FileName)));
        }

        [Fact]
        public async void DeleteUserPhotoCommandHandlerFailOnWrongId()
        {
            DeleteUserPhotoCommandHandler handler = new DeleteUserPhotoCommandHandler(Database, FileManager, Cache, Logger);
            DeleteUserPhotoCommand command = new DeleteUserPhotoCommand();


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

        private async Task<UserPhoto> CreateAndAddUserPhotoToDatabase(Guid userId)
        {
            UserPhoto userPhoto = new UserPhoto()
            {
                UserId = userId,
                Id = Guid.NewGuid(),
                FileName = "test-user-photo.jpg"
            };

            await File.WriteAllBytesAsync(Path.Combine(FileManager.UserSavePhotoPath, userPhoto.FileName), Photo.Bytes);

            Database.UserPhotos.Add(userPhoto);
            await Database.SaveChangesAsync();

            return userPhoto;
        }
    }
}
