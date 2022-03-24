using Medium.Users.Application.Handlers.Users.Commands.CreateUser;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Tests.Base;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : BaseCommandTests
    {
        [Fact]
        public async void CreateUserCommandHandlerSuccess()
        {
            ILogger<CreateUserCommandHandler> logger = new Mock<ILogger<CreateUserCommandHandler>>().Object;
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Database, logger);
            CreateUserCommand command = new CreateUserCommand()
            {
                Name = "test-name",
                Email = "test-email",
                Password = "test-password",
            };


            Guid userId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.Users.FindAsync(userId));
        }

        [Fact]
        public async void CreateUserCommandHandlerFailOnWrongEmail()
        {
            User user = await CreateAndAddUserToDatabase();
            ILogger<CreateUserCommandHandler> logger = new Mock<ILogger<CreateUserCommandHandler>>().Object;
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Database, logger);
            CreateUserCommand command = new CreateUserCommand()
            {
                Name = "test-name",
                Email = user.Email,
                Password = "test-password",
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<User> CreateAndAddUserToDatabase()
        {
            User user = new User() 
            {
                Name = "test-name",
                Email = "test-email",
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}
