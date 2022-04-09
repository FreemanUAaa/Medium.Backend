using MediatR;
using Medium.Desings.Core.Exceptions;
using Medium.Desings.Core.Interfaces;
using Medium.Desings.Core.Interfaces.Interfaces;
using Medium.Desings.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Desings.Application.Handlers.Desings.Commands.CreateDesing
{
    public class CreateDesingCommandHandler : IRequestHandler<CreateDesingCommand, Guid>
    {
        private readonly ILogger<CreateDesingCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        public CreateDesingCommandHandler(IDatabaseContext database, IFileManager fileManager, ILogger<CreateDesingCommandHandler> logger) =>
            (this.database, this.fileManager, this.logger) = (database, fileManager, logger);


        public async Task<Guid> Handle(CreateDesingCommand request, CancellationToken cancellationToken)
        {
            if (database.Desings.Any(x => x.UserId == request.UserId))
            {
                throw new Exception(ExceptionStrings.ModelAlreadyExists);
            }

            Guid desingId = Guid.NewGuid();
            Guid headerId = Guid.NewGuid();

            MainColors mainColors = new MainColors()
            {
                DesingId = desingId,
                AccentRgb = "#1a8917ff",
                BackgroundRgb = "#ffffffff",
            };

            MainFonts mainFonts = new MainFonts()
            {
                DesingId = desingId,
                BodyFontId = Guid.NewGuid(),
                TitleFontId = Guid.NewGuid(),
                DetailsFontId = Guid.NewGuid(),
            };

            await database.SaveChangesAsync();

            return desingId;
        }
    }
}
