using Medium.Users.Application.Common;
using Medium.Users.Application.Handlers.Users.Queries.LoginUser;
using Medium.Users.Core.Common.Password;
using Medium.Users.Core.Models;
using Medium.Users.Tests.Tests.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Users.Tests.Tests.Users.Queries
{
    public class LoginUserQueryHandlerTests : BaseCommandTests
    {
        [Fact]
        public async void LoginUserQueryHandlerSuccess()
        {
            byte[] salt = PasswordHasher.GetNewSalt();
            string hashPassword = PasswordHasher.HashPassword("test-password", salt).Hash;
            User user = await CreateAndAddUserToDatabase("test-password", salt);
            AuthOptions authOptions = new AuthOptions()
            {
                Issuer = "test-issuer", Audience = "test-audience",
                Key = "test-key", Lifetime = 120,
            };
            IOptions<AuthOptions> options = new Mock<IOptions<AuthOptions>>().SetupProperty(x => x.Value, authOptions).Object;
            ILogger<LoginUserQueryHandler> logger = new Mock<ILogger<LoginUserQueryHandler>>().Object;
            LoginUserQueryHandler handler = new LoginUserQueryHandler(Database, logger, options);
            LoginUserQuery query = new LoginUserQuery()
            {
                Email = user.Email,
                Password = user.Email,
            };


            LoginUserVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(vm);
            Assert.NotEmpty(vm.AccessToken);
            vm.UserId.ShouldBe(user.Id);
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
