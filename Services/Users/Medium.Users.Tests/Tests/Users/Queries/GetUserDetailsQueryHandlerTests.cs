using Medium.Users.Application.Handlers.Users.Queries.GetUserDetails;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Tests.Base;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Queries
{
    public class GetUserDetailsQueryHandlerTests : BaseQueryTests
    {
        [Fact]
        public async void GetUserDetailsQueryHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            ILogger<GetUserDetailsQueryHandler> logger = new Mock<ILogger<GetUserDetailsQueryHandler>>().Object;
            GetUserDetailsQueryHandler handler = new GetUserDetailsQueryHandler(Database, Mapper, logger);
            GetUserDetailsQuery query = new GetUserDetailsQuery() { UserId = user.Id };


            GetUserDetailsVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(vm);
        }

        [Fact]
        public async void GetUserDetailsQueryHandlerFailOnWrongUserId()
        {
            ILogger<GetUserDetailsQueryHandler> logger = new Mock<ILogger<GetUserDetailsQueryHandler>>().Object;
            GetUserDetailsQueryHandler handler = new GetUserDetailsQueryHandler(Database, Mapper, logger);
            GetUserDetailsQuery query = new GetUserDetailsQuery();


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
    }
}
