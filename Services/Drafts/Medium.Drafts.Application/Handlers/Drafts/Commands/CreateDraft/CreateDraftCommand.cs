using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.CreateDraft
{
    public class CreateDraftCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string Body { get; set; }

        public int WordCount { get; set; }
    }
}
