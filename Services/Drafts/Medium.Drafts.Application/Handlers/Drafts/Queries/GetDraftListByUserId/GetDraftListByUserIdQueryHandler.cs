using MediatR;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftListByUserId
{
    public class GetDraftListByUserIdQueryHandler : IRequestHandler<GetDraftListByUserIdQuery, GetDraftListVm>
    {
        private readonly ILogger<GetDraftListByUserIdQueryHandler> logger;

        private readonly IDatabaseContext database;

        public GetDraftListByUserIdQueryHandler(IDatabaseContext database, ILogger<GetDraftListByUserIdQueryHandler> logger = null) =>
            (this.database, this.logger) = (database, logger);

        public async Task<GetDraftListVm> Handle(GetDraftListByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Draft> drafts = await database.Drafts.Where(x => x.UserId == request.UserId).ToListAsync();

            logger.LogInformation("The draft was received successfully");

            return new GetDraftListVm() { Drafts = drafts };
        }
    }
}
