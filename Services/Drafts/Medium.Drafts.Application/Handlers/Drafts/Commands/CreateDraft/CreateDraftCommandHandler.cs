using MediatR;
using Medium.Drafts.Core.Common.ReadTime;
using Medium.Drafts.Core.Exceptions;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Core.Redis;
using Medium.Drafts.GrpcClient.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.CreateDraft
{
    public class CreateDraftCommandHandler : IRequestHandler<CreateDraftCommand, Guid>
    {
        private readonly ILogger<CreateDraftCommandHandler> logger;

        private readonly IGrpcUserService grpcUserService;

        private readonly IDatabaseContext database;

        private readonly IDistributedCache cache;

        public CreateDraftCommandHandler(IDatabaseContext database, IDistributedCache cache, IGrpcUserService grpcUserService, ILogger<CreateDraftCommandHandler> logger) =>
            (this.database, this.cache, this.grpcUserService, this.logger) = (database, cache, grpcUserService, logger);

        public async Task<Guid> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
        {
            if (!await grpcUserService.IsExistsAsync(request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            TimeSpan readTime = ReadTime.Get(request.WordCount);

            Draft draft = new Draft() 
            { 
                Id = Guid.NewGuid(),
                Details = request.Details,
                Title = request.Title,
                ReadTime = readTime,
                LastEditDate = DateTime.Now,
                UserId = request.UserId,
                Body = request.Body,
                WordCount = request.WordCount,
            };

            database.Drafts.Add(draft);
            await database.SaveChangesAsync(cancellationToken);

            await cache.RemoveAsync(RedisKeys.GetDraftDetailsKey(request.UserId));

            logger.LogInformation("The draft was successfully created");

            return draft.Id;
        }
    }
}
