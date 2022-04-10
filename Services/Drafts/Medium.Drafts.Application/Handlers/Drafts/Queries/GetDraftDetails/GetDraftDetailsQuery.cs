using MediatR;
using Medium.Drafts.Core.Models;
using System;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftDetails
{
    public class GetDraftDetailsQuery : IRequest<Draft>
    {
        public Guid DraftId { get; set; }

        public Guid UserId { get; set; }
    }
}
