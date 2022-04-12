using Medium.Drafts.Application.Handlers.Drafts.Commands.UpdateDraft;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Tests.Tests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Medium.Drafts.Tests.Tests.Drafts.Commands
{
    public class UpdateDraftCommandHandlerTests : BaseCommandTests<UpdateDraftCommandHandler>
    {
        [Fact]
        public async void UpdateDraftCommandHandlerSuccess()
        {
            Draft draft = await CreateAndSaveTestDraft();
            UpdateDraftCommandHandler handler = new UpdateDraftCommandHandler(Database, Cache, Logger);
            UpdateDraftCommand command = new UpdateDraftCommand()
            {
                DraftId = draft.Id,
                Body = "new-body",
            };


            await handler.Handle(command, CancellationToken.None);


            Draft newDraft = await Database.Drafts.FindAsync(draft.Id);
            Assert.NotNull(newDraft);
            newDraft.Body.ShouldBe(command.Body);
        }

        [Fact]
        public async void UpdateDraftCommandHandlerFailOnWrongId()
        {
            UpdateDraftCommandHandler handler = new UpdateDraftCommandHandler(Database, Cache, Logger);
            UpdateDraftCommand command = new UpdateDraftCommand();


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
