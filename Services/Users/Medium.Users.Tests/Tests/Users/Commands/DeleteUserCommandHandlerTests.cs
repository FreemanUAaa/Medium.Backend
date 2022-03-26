using Medium.Users.Application.Handlers.Users.Commands.DeleteUser;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Assets;
using Medium.Users.Tests.Tests.Base;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests : BaseCommandTests<DeleteUserCommandHandler>
    {
        [Fact]
        public async void DeleteUserCommandHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            UserPhoto userPhoto = await CreateAndAddUserPhotoToDatabase(user.Id);
            BioPhoto bioPhoto = await CreateAndAddBioPhotoToDatabase(user.Id);
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(Database, FileManager, Cache, Logger);
            DeleteUserCommand command = new DeleteUserCommand() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.Users.FindAsync(user.Id));
            Assert.Null(await Database.BioPhotos.FindAsync(bioPhoto.Id));
            Assert.Null(await Database.UserPhotos.FindAsync(userPhoto.Id));
            Assert.False(File.Exists(Path.Combine(FileManager.BioSavePhotoPath, bioPhoto.FileName)));
            Assert.False(File.Exists(Path.Combine(FileManager.UserSavePhotoPath, bioPhoto.FileName)));
        }

        [Fact]
        public async void DeleteUserCommandHandlerFailOnwrongUserdId()
        {
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(Database, FileManager, Cache, Logger);
            DeleteUserCommand command = new DeleteUserCommand();


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

        private async Task<BioPhoto> CreateAndAddBioPhotoToDatabase(Guid userId)
        {
            BioPhoto bioPhoto = new BioPhoto()
            {
                UserId = userId,
                Id = Guid.NewGuid(),
                FileName = "test-bio-photo.jpg"
            };

            await File.WriteAllBytesAsync(Path.Combine(FileManager.BioSavePhotoPath, bioPhoto.FileName), Photo.Bytes);

            Database.BioPhotos.Add(bioPhoto);
            await Database.SaveChangesAsync();

            return bioPhoto;
        }
    }
}
