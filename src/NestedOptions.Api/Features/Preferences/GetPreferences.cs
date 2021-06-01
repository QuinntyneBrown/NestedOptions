using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NestedOptions.Api.Features
{
    public class GetPreferences
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<PreferencesDto> Preferences { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly INestedOptionsDbContext _context;
        
            public Handler(INestedOptionsDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Preferences = await _context.Preferences.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
