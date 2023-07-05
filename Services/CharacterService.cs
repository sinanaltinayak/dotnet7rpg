namespace dotnet7rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(s => _mapper.Map<GetCharacterDto>(s)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var character = await _context.Characters.FirstOrDefaultAsync(f => f.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(s => _mapper.Map<GetCharacterDto>(s)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(f => f.Id == updatedCharacter.Id);

                if (character is null)
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

                character.Name = updatedCharacter.Name;
                character.Vitality = updatedCharacter.Vitality;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(f => f.Id == id);

                if (character is null)
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Characters.Select(s => _mapper.Map<GetCharacterDto>(s)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}