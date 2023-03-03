using Microsoft.AspNetCore.Mvc;
using RateMyAnimal.Application.Users.Commands.CreateUser;
using RateMyAnimal.Application.Users.Queries.GetUserHistory;

namespace RateMyAnimal.API.Controllers;

public class UserController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserHistoryDto>> GetHistory(int id)
    {
        return await Mediator.Send(new GetUserHistoryQuery { UsertId = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
