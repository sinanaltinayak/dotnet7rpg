using Microsoft.AspNetCore.Mvc;

namespace dotnet7rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto character)
        {
            var response = await _characterService.UpdateCharacter(character);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}