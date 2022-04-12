using Medium.Drafts.Application.Handlers.Drafts.Commands.DeleteDraft;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Tests.Tests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medium.Drafts.Tests.Tests.Drafts.Commands
{
    public class DeleteDraftCommandHandlerTests : BaseCommandTests<DeleteDraftCommandHandler>
    {
        [Fact]
        public async void DeleteDraftCommandHandlerSuccess()
        {
            Draft draft = await CreateAndSaveTestDraft();
            DeleteDraftCommandHandler handler = new DeleteDraftCommandHandler(Database, Cache, Logger);
            DeleteDraftCommand command = new DeleteDraftCommand()
            {
                UserId = draft.UserId,
                DraftId = draft.Id,
            };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.Drafts.FindAsync(draft.Id));
        }

        [Fact]
        public async void DeleteDraftCommandHandlerFailOnWrongId()
        {
            DeleteDraftCommandHandler handler = new DeleteDraftCommandHandler(Database, Cache, Logger);
            DeleteDraftCommand command = new DeleteDraftCommand();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<Draft> CreateAndSaveTestDraft()
        {
            Draft draft = new Draft()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };

            Database.Drafts.Add(draft);
            await Database.SaveChangesAsync();

            return draft;
        }
    }
}
