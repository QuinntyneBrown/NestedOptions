using MediatR;
using Microsoft.EntityFrameworkCore;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NestedOptions.Api.Features
{
    public class RemoveUser
    {
        public class Request : IRequest<Response>
        {
            public Guid UserId { get; set; }
        }

        public class Response : ResponseBase
        {
            public UserDto User { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly INestedOptionsDbContext _context;

            public Handler(INestedOptionsDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.UserId == request.UserId);

                _context.Users.Remove(user);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    User = user.ToDto()
                };
            }

        }
    }
}
