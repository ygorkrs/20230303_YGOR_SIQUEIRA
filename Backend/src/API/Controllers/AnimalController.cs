using Microsoft.AspNetCore.Mvc;
using RateMyAnimal.Application.Animals.Commands.CreateAnimal;
using RateMyAnimal.Application.Animals.Commands.UpdateAnimal;
using RateMyAnimal.Application.Animals.Queries.GetAnimal;
using RateMyAnimal.Application.Animals.Queries.GetNewAnimal;

namespace RateMyAnimal.API.Controllers;

public class AnimalController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<AnimalGetDto>> Get(int id)
    {
        return await Mediator.Send(new GetAnimalQuery { Id = id });
    }

    [HttpGet]
    public async Task<ActionResult<NewAnimalDto>> GetNew()
    {
        var newAnimalDto = await Mediator.Send(new GetNewAnimalQuery());

        if (newAnimalDto.AnimalOriginUrlDto.NeedRetrieveUrlInBody)
        {
            newAnimalDto.AnimalOriginUrlDto.Url = await GetRealUrlPhoto(newAnimalDto.AnimalOriginUrlDto.Url);
        }

        newAnimalDto.Photo = await GetBytesPhoto(newAnimalDto.AnimalOriginUrlDto.Url);

        return Ok(newAnimalDto);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateAnimalCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAnimal(int id, UpdateAnimalCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    private async Task<string> GetRealUrlPhoto(string url)
    {
        string newUrl = string.Empty;

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            newUrl = responseContent.Replace("[", "").Replace("]", "").Replace("\"", "");
        };

        return newUrl;
    }

    private async Task<byte[]> GetBytesPhoto(string url)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        };
    }
}
