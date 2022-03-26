using Medium.Users.Application.Handlers.BioPhotos.Commands.DeleteBioPhoto;
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

namespace Medium.Users.Tests.Tests.BioPhotos.Commands
{
    public class DeleteBioPhotoCommandHandlerTests : BaseCommandTests<DeleteBioPhotoCommandHandler>
    {
        [Fact]
        public async void DeleteBioPhotoCommandHandlerSuccess()
        {
            BioPhoto bioPhoto = await CreateAndAddBioPhotoToDatabase();
            DeleteBioPhotoCommandHandler handler = new DeleteBioPhotoCommandHandler(Database, FileManager, Cache, Logger);
            DeleteBioPhotoCommand command = new DeleteBioPhotoCommand() { BioPhotoId = bioPhoto.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.BioPhotos.FindAsync(bioPhoto.Id));
            Assert.False(File.Exists(Path.Combine(FileManager.BioSavePhotoPath, bioPhoto.FileName)));
        }

        [Fact]
        public async void DeleteBioPhotoCommandHandlerFailOnWrongId()
        {
            DeleteBioPhotoCommandHandler handler = new DeleteBioPhotoCommandHandler(Database, FileManager, Cache, Logger);
            DeleteBioPhotoCommand command = new DeleteBioPhotoCommand();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
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
