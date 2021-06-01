using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NestedOptions.Api.Features
{
    public class GetPreferencesById
    {
        public class Request: IRequest<Response>
        {
            public Guid PreferencesId { get; set; }
        }

        public class Response: ResponseBase
        {
            public PreferencesDto Preferences { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly INestedOptionsDbContext _context;
        
            public Handler(INestedOptionsDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Preferences = (await _context.Preferences.SingleOrDefaultAsync(x => x.PreferencesId == request.PreferencesId)).ToDto()
                };
            }
            
        }
    }
}
