using dotnet7rpg.Dtos.Weapon;

namespace dotnet7rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon); 
    }
}