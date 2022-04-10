using MediatR;
using System;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommand : IRequest
    {
        public Guid DraftId { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string Body { get; set; }

        public int WordCount { get; set; }
    }
}
