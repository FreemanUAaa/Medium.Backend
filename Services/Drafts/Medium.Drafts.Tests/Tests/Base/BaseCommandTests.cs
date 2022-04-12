using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.GrpcClient.Interfaces;
using Medium.Drafts.Tests.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Medium.Drafts.Tests.Tests.Base
{
    public abstract class BaseCommandTests<TLogger> where TLogger : class
    {
        public readonly IGrpcUserService grpcUserService;

        public readonly IDatabaseContext Database;

        public readonly IDistributedCache Cache;

        public readonly ILogger<TLogger> Logger;

        public readonly Guid CorrectUserId;

        public BaseCommandTests()
        {
            CorrectUserId = Guid.NewGuid();

            Database = DatabaseContextFactory.Create();

            Cache = new Mock<IDistributedCache>().Object;

            Logger = new Mock<ILogger<TLogger>>().Object;

            Mock<IGrpcUserService> grpcUserServiceMock = new Mock<IGrpcUserService>();
            grpcUserServiceMock.Setup(x => x.IsExistsAsync(It.IsAny<Guid>()))
                               .ReturnsAsync((Guid userId) => userId == CorrectUserId);

            grpcUserService = grpcUserServiceMock.Object;
        }
    }
}
