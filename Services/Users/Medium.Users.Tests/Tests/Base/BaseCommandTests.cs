using Medium.Users.Application.Services;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Tests.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;

namespace Medium.Users.Tests.Tests.Base
{
    public abstract class BaseCommandTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public readonly IDistributedCache Cache;

        public readonly ILogger<TLogger> Logger;

        public BaseCommandTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager(
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\UserPhotos\",
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\BioPhotos\"
            );

            Cache = new Mock<IDistributedCache>().Object;

            Logger = new Mock<ILogger<TLogger>>().Object;
        }
    }
}
