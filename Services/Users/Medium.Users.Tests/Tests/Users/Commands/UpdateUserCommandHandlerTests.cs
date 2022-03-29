using Medium.Users.Application.Handlers.Users.Commands.UpdateUser;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Tests.Base;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Commands
{
    public class UpdateUserCommandHandlerTests : BaseCommandTests<UpdateUserCommandHandler>
    {
        [Fact]
        public async void UpdateUserCommandHandlerSuccess()
        {
            User user = await CreateAndAddUserToDatabase();
            UpdateUserCommandHandler handler = new UpdateUserCommandHandler(Database, Logger);
            UpdateUserCommand command = new UpdateUserCommand()
            {
                UserId = user.Id,
                Bio = "new-test-bio",
                Email = "new-test-email",
            };


            await handler.Handle(command, CancellationToken.None);


            User newUser = await Database.Users.FindAsync(user.Id);
            newUser.Email.ShouldBe("new-test-email");
            newUser.Bio.ShouldBe("new-test-bio");
        }

        [Fact]
        public async void UpdateUserCommandHandlerFailOnWrongId()
        {
            UpdateUserCommandHandler handler = new UpdateUserCommandHandler(Database, Logger);
            UpdateUserCommand command = new UpdateUserCommand();


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
    }
}
