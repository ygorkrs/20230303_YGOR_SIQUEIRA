using Microsoft.AspNetCore.Mvc;
using RateMyAnimal.Application.Tags.Commands.CreateTag;
using RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;

namespace RateMyAnimal.API.Controllers;

public class TagController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ListTagsDto>> GetAvailable()
    {
        return await Mediator.Send(new GetTagsAvailableQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }
}
