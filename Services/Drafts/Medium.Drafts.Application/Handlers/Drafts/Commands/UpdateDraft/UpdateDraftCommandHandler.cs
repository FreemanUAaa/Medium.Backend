using MediatR;
using Medium.Drafts.Core.Common.ReadTime;
using Medium.Drafts.Core.Exceptions;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Core.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommandHandler : IRequestHandler<UpdateDraftCommand>
    {
        private readonly ILogger<UpdateDraftCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IDistributedCache cache;

        public UpdateDraftCommandHandler(IDatabaseContext database, IDistributedCache cache, ILogger<UpdateDraftCommandHandler> logger) =>
            (this.database, this.cache, this.logger) = (database, cache, logger);

        public async Task<Unit> Handle(UpdateDraftCommand request, CancellationToken cancellationToken)
        {
            Draft draft = await database.Drafts.FindAsync(request.DraftId);

            if (draft == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            (draft.Title, draft.Details, draft.Body, draft.WordCount) =
                (request.Title, request.Details, request.Body, request.WordCount);

            TimeSpan readTime = ReadTime.Get(request.WordCount);

            (draft.ReadTime, draft.LastEditDate) = (readTime, DateTime.Now);

            database.Drafts.Update(draft);
            await database.SaveChangesAsync(cancellationToken);

            await cache.RemoveAsync(RedisKeys.GetDraftDetailsKey(request.DraftId));

            logger.LogInformation("The draft was successfully updated");

            return Unit.Value;
        }
    }
}
