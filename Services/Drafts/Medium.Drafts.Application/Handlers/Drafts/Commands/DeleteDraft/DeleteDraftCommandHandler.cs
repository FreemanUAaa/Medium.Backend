using MediatR;
using Medium.Drafts.Core.Exceptions;
using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommandHandler : IRequestHandler<DeleteDraftCommand>
    {
        private readonly ILogger<DeleteDraftCommand> logger;

        private readonly IDatabaseContext database;

        public DeleteDraftCommandHandler(IDatabaseContext database, ILogger<DeleteDraftCommand> logger = null) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Unit> Handle(DeleteDraftCommand request, CancellationToken cancellationToken)
        {
            Draft draft = await database.Drafts.FirstOrDefaultAsync(x => x.Id == request.DraftId && x.UserId == request.UserId);

            if (draft == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            database.Drafts.Remove(draft);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Draft deleted successfully");

            return Unit.Value;    
        }
    }
}
