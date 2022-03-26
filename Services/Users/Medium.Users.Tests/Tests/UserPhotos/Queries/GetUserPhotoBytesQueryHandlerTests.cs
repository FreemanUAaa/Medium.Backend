using Medium.Users.Application.Handlers.UserPhotos.Queries.GetUserPhotoBytes;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Assets;
using Medium.Users.Tests.Tests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.UserPhotos.Queries
{
    public class GetUserPhotoBytesQueryHandlerTests : BaseQueryTests<GetUserPhotoBytesQueryHandler>
    {
        [Fact]
        public async void GetUserPhotoBytesQueryHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            await CreateAndAddUserPhotoToDatabase(user.Id);
            GetUserPhotoBytesQueryHandler handler = new GetUserPhotoBytesQueryHandler(Database, FileManager, Logger);
            GetUserPhotoBytesQuery query = new GetUserPhotoBytesQuery() { UserId = user.Id };


            byte[] bytes = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(bytes);
            bytes.Length.ShouldNotBe(0);
        }

        [Fact]
        public async void GetUserPhotoBytesQueryHandlerFailOnWrongId()
        {
            GetUserPhotoBytesQueryHandler handler = new GetUserPhotoBytesQueryHandler(Database, FileManager, Logger);
            GetUserPhotoBytesQuery query = new GetUserPhotoBytesQuery();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
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
