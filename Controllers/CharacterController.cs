using Microsoft.AspNetCore.Mvc;

namespace dotnet7rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> knight = new List<Character>{
            new Character(),
            new Character {
                Id = 1,
                Name = "Player2"
            }
        };

        [HttpGet]
        public ActionResult<Character> Get(int id)
        {
            return Ok(knight.FirstOrDefault(f => f.Id == id));
        }

        [HttpGet]
        public ActionResult<List<Character>> GetAll()
        {
            return Ok(knight);
        }

        [HttpPost]
        public ActionResult<int> Create(Character character)
        {
            knight.Add(character);
            return Ok(character.Id);
        }
    }
}