using Castle.Core.Logging;
using Medium.Users.Application.Services;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Tests.Database;
using Moq;

namespace Medium.Users.Tests.Tests.Base
{
    public abstract class BaseQueryTests
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager(
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\UserPhotos\",
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\BioPhotos\"
            );
        }
    }
}
