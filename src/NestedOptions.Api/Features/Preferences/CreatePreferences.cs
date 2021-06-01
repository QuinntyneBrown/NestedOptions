using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NestedOptions.Api.Models;
using NestedOptions.Api.Core;
using NestedOptions.Api.Interfaces;

namespace NestedOptions.Api.Features
{
    public class CreatePreferences
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Preferences).NotNull();
                RuleFor(request => request.Preferences).SetValidator(new PreferencesValidator());
            }

        }

        public class Request : IRequest<Response>
        {
            public PreferencesDto Preferences { get; set; }
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
                var preferences = new Preferences(request.Preferences.AllowSocialSignIn, request.Preferences.AllowMultipleLanguages);

                _context.Preferences.Add(preferences);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Preferences = preferences.ToDto()
                };
            }

        }
    }
}
