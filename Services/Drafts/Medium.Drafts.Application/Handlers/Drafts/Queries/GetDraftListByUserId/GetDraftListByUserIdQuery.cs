using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftListByUserId
{
    public class GetDraftListByUserIdQuery : IRequest<GetDraftListVm>
    {
        public Guid UserId { get; set; }
    }
}
