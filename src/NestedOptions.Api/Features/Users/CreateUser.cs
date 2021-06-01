using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NestedOptions.Api.Models;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;

namespace NestedOptions.Api.Features
{
    public class CreateUser
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.User).NotNull();
                RuleFor(request => request.User).SetValidator(new UserValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public UserDto User { get; set; }
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
                var user = new User(request.User.Username, request.User.IsAdmin, new(request.User.Preferences.AllowSocialSignIn, request.User.Preferences.AllowMultipleLanguages));

                _context.Users.Add(user);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    User = user.ToDto()
                };
            }
        }
    }
}
