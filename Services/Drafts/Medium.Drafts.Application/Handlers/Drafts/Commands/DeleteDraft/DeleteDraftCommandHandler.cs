using MediatR;
using Medium.Drafts.Core.Exceptions;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Core.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommandHandler : IRequestHandler<DeleteDraftCommand>
    {
        private readonly ILogger<DeleteDraftCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IDistributedCache cache;

        public DeleteDraftCommandHandler(IDatabaseContext database, IDistributedCache cache, ILogger<DeleteDraftCommandHandler> logger = null) =>
            (this.database, this.cache, this.logger) = (database, cache, logger);

        public async Task<Unit> Handle(DeleteDraftCommand request, CancellationToken cancellationToken)
        {
            Draft draft = await database.Drafts.FirstOrDefaultAsync(x => x.Id == request.DraftId && x.UserId == request.UserId);

            if (draft == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            database.Drafts.Remove(draft);
            await database.SaveChangesAsync(cancellationToken);

            await cache.RemoveAsync(RedisKeys.GetDraftDetailsKey(request.DraftId));

            logger.LogInformation("Draft deleted successfully");

            return Unit.Value;    
        }
    }
}
