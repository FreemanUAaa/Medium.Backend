using Medium.Users.Application.Common;
using Medium.Users.Application.Handlers.Users.Queries.LoginUser;
using Medium.Users.Core.Common.Password;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Tests.Base;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Queries
{
    public class LoginUserQueryHandlerTests : BaseCommandTests<LoginUserQueryHandler>
    {
        private readonly IOptions<AuthOptions> authOptions;

        public LoginUserQueryHandlerTests()
        {
            AuthOptions auth = new AuthOptions()
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                Key = "mysupersecret_secretkey!123",
                Lifetime = 120,
            };

            authOptions = Options.Create(auth);
        }

        [Fact]
        public async void LoginUserQueryHandlerSuccess()
        {
            byte[] salt = PasswordHasher.GetNewSalt();
            User user = await CreateAndAddUserToDatabase("test-password", salt);
            LoginUserQueryHandler handler = new LoginUserQueryHandler(Database, Logger, authOptions);
            LoginUserQuery query = new LoginUserQuery()
            {
                Email = user.Email,
                Password = "test-password",
            };


            LoginUserVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(vm);
            Assert.NotEmpty(vm.AccessToken);
            vm.UserId.ShouldBe(user.Id);
        }

        [Fact]
        public async void LoginUserQueryHandlerFailOnWrongUserData()
        {
            LoginUserQueryHandler handler = new LoginUserQueryHandler(Database, Logger, authOptions);
            LoginUserQuery query = new LoginUserQuery()
            {
                Email = "test-email",
                Password = "test-password",
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }

        private async Task<User> CreateAndAddUserToDatabase(string password, byte[] salt)
        {
            string hashPassword = PasswordHasher.HashPassword(password, salt).Hash;

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "test-name",
                Email = "test-email",
                PasswordSalt = salt,
                PasswordHash = hashPassword,
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}
