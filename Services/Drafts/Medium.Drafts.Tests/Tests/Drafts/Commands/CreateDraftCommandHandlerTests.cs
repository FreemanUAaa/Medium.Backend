using Medium.Drafts.Application.Handlers.Drafts.Commands.CreateDraft;
using Medium.Drafts.Tests.Tests.Base;
using System;
using System.Threading;
using Xunit;

namespace Medium.Drafts.Tests.Tests.Drafts.Commands
{
    public class CreateDraftCommandHandlerTests : BaseCommandTests<CreateDraftCommandHandler>
    {
        [Fact]
        public async void CreateDraftCommandHandlerSuccess()
        {
            CreateDraftCommandHandler handler = new CreateDraftCommandHandler(Database, Cache, grpcUserService, Logger);
            CreateDraftCommand command = new CreateDraftCommand()
            {
                UserId = CorrectUserId,
                Title = "test-title",
                Details = "test-details",
                Body = "test-body",
                WordCount = 120,
            };


            Guid draftId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.Drafts.FindAsync(draftId));
        }  

        [Fact]
        public async void CreateDraftCommandHandlerFailOnWrongUserId()
        {
            CreateDraftCommandHandler handler = new CreateDraftCommandHandler(Database, Cache, grpcUserService, Logger);
            CreateDraftCommand command = new CreateDraftCommand();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
