using AutoMapper;

namespace NZWallks.Profiles
{
    public class WalkProfile :Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Wallk>()
                .ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty,Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }

        
    }
}
