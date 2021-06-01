using MediatR;
using Microsoft.EntityFrameworkCore;
using NestedOptions.Api.Core;
using NestedOptions.Api.Extensions;
using NestedOptions.Api.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NestedOptions.Api.Features
{
    public class GetPreferencesPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<PreferencesDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly INestedOptionsDbContext _context;
        
            public Handler(INestedOptionsDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from p in _context.Preferences
                    select p;
                
                var length = await _context.Preferences.CountAsync();
                
                var preferences = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = preferences
                };
            }
            
        }
    }
}
