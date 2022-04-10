using MediatR;
using System;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommand : IRequest
    {
        public Guid DraftId { get; set; }

        public Guid UserId { get; set; }
    }
}
