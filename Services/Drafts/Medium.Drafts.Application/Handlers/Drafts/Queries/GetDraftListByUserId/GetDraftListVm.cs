using Medium.Drafts.Core.Models;
using System.Collections.Generic;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftListByUserId
{
    public class GetDraftListVm
    {
        public List<Draft> Drafts { get; set; }
    }
}
