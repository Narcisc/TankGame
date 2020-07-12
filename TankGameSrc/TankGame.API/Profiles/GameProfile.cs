using TankGame.API.Helpers;
using AutoMapper;

namespace TankGame.API.Profiles
{
    /// <summary>
    /// Define rules for mapping entity - view model
    /// </summary>
    public class GameProfile :  Profile
    {
        public GameProfile()
        {
            _ = CreateMap<Entities.TankModel, Models.TankModelDto>();
            _ = CreateMap<Models.TankModelDto, Entities.TankModel>();
            _ = CreateMap<Entities.GameMap, Models.GameMapDto>()
                .ForMember(dest => dest.Map,
                opt => opt.MapFrom(src => src.Map.ToMatrix()));
            _ = CreateMap<Models.GameMapDto, Entities.GameMap>()
                .ForMember(dest => dest.Map,
                opt => opt.MapFrom(src => src.Map.ToJson()));
            _ = CreateMap<Entities.GameBattle, Models.GameBattleDto>();
            _ = CreateMap<Models.GameBattleDto, Entities.GameBattle>();
            _ = CreateMap<Entities.GameSimulation, Models.GameSimulationDto>();
            _ = CreateMap<Models.GameSimulationDto, Entities.GameSimulation>();
        }
    }
}
