using AutoMapper;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Models.DTO.Difficulty;
using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //Regions Mapping
            CreateMap<Region, RegionsDTO>().ReverseMap();
            CreateMap<CreateRegionsRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionsRequestDTO, Region>().ReverseMap();

            //Walk mapping
            CreateMap<Walk, WalksDTO>().ReverseMap();
            CreateMap<CreateWalksRequestDTO, Walk>().ReverseMap();
            CreateMap<UpdateWalksRequestDTO, Walk>().ReverseMap();

            // Difficulty mapping
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<CreateDifficultyRequestDTO,Walk>().ReverseMap();
            CreateMap<UpdateDifficultyRequestDTO,Difficulty>().ReverseMap();

           

        }
    }
}
