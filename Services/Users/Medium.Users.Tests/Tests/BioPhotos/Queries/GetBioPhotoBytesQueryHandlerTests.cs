using Medium.Users.Application.Handlers.BioPhotos.Queries.GetBioPhotoBytes;
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

namespace Medium.Users.Tests.Tests.BioPhotos.Queries
{
    public class GetBioPhotoBytesQueryHandlerTests : BaseQueryTests<GetBioPhotoBytesQueryHandler>
    {
        [Fact]
        public async void GetBioPhotoBytesQueryHandlerSuccess()
        {
            BioPhoto bioPhoto = await CreateAndAddBioPhotoToDatabase();
            GetBioPhotoBytesQueryHandler handler = new GetBioPhotoBytesQueryHandler(Database, FileManager, Logger);
            GetBioPhotoBytesQuery query = new GetBioPhotoBytesQuery() { BioPhotoId = bioPhoto.Id };


            byte[] bytes = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(bytes);
            bytes.Length.ShouldNotBe(0);
        }

        [Fact]
        public async void GetBioPhotoBytesQueryHandlerFailOnWrongId()
        {
            GetBioPhotoBytesQueryHandler handler = new GetBioPhotoBytesQueryHandler(Database, FileManager, Logger);
            GetBioPhotoBytesQuery query = new GetBioPhotoBytesQuery();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }

        private async Task<BioPhoto> CreateAndAddBioPhotoToDatabase()
        {
            BioPhoto bioPhoto = new BioPhoto()
            {
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
