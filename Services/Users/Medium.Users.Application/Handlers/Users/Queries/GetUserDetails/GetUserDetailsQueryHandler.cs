using MediatR;
using Medium.Users.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Medium.Users.Core.Models;
using Medium.Users.Core.Exceptions;
using AutoMapper;

namespace Medium.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDetailsVm>
    {
        private readonly ILogger<GetUserDetailsQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IMapper mapper;

        public GetUserDetailsQueryHandler(IDatabaseContext database, IMapper mapper, ILogger<GetUserDetailsQueryHandler> logger) =>
            (this.database, this.mapper, this.logger) = (database, mapper, logger);

        public async Task<GetUserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            GetUserDetailsVm vm = mapper.Map<GetUserDetailsVm>(user);

            logger.LogInformation("User successfully received");

            return vm;
        }
    }
}
