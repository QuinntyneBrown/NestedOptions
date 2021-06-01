using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using NestedOptions.Api.Models;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;

namespace NestedOptions.Api.Features
{
    public class RemovePreferences
    {
        public class Request : IRequest<Response>
        {
            public Guid PreferencesId { get; set; }
        }

        public class Response : ResponseBase
        {
            public PreferencesDto Preferences { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly INestedOptionsDbContext _context;

            public Handler(INestedOptionsDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var preferences = await _context.Preferences.SingleAsync(x => x.PreferencesId == request.PreferencesId);

                _context.Preferences.Remove(preferences);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Preferences = preferences.ToDto()
                };
            }

        }
    }
}
