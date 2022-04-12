using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Tests.Database;
using Microsoft.Extensions.Logging;
using Moq;

namespace Medium.Drafts.Tests.Tests.Base
{
    public abstract class BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly ILogger<TLogger> Logger;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            Logger = new Mock<ILogger<TLogger>>().Object;
        }
    }
}
