using System.Net;
using System.Threading.Tasks;
using NestedOptions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NestedOptions.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreferencesController
    {
        private readonly IMediator _mediator;

        public PreferencesController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{preferencesId}", Name = "GetPreferencesByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPreferencesById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPreferencesById.Response>> GetById([FromRoute] GetPreferencesById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Preferences == null)
            {
                return new NotFoundObjectResult(request.PreferencesId);
            }

            return response;
        }

        [HttpGet(Name = "GetPreferencesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPreferences.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPreferences.Response>> Get()
            => await _mediator.Send(new GetPreferences.Request());

        [HttpPost(Name = "CreatePreferencesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePreferences.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePreferences.Response>> Create([FromBody] CreatePreferences.Request request)
            => await _mediator.Send(request);

        [HttpGet("page/{pageSize}/{index}", Name = "GetPreferencesPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPreferencesPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPreferencesPage.Response>> Page([FromRoute] GetPreferencesPage.Request request)
            => await _mediator.Send(request);

        [HttpPut(Name = "UpdatePreferencesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePreferences.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePreferences.Response>> Update([FromBody] UpdatePreferences.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{preferencesId}", Name = "RemovePreferencesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemovePreferences.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemovePreferences.Response>> Remove([FromRoute] RemovePreferences.Request request)
            => await _mediator.Send(request);

    }
}
