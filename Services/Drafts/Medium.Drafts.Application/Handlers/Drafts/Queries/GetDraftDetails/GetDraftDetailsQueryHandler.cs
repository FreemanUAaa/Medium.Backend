using MediatR;
using Medium.Drafts.Core.Exceptions;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftDetails
{
    public class GetDraftDetailsQueryHandler : IRequestHandler<GetDraftDetailsQuery, Draft>
    {
        private readonly ILogger<GetDraftDetailsQueryHandler> logger;

        private readonly IDatabaseContext database;

        public GetDraftDetailsQueryHandler(IDatabaseContext database, ILogger<GetDraftDetailsQueryHandler> logger = null) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Draft> Handle(GetDraftDetailsQuery request, CancellationToken cancellationToken)
        {
            Draft draft = await database.Drafts.FirstOrDefaultAsync(x => x.Id == request.DraftId && x.UserId == request.UserId);

            if (draft == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            logger.LogInformation("The draft was received successfully");

            return draft;
        }
    }
}
