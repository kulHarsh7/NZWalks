namespace NZWalks.API.Models.DTO.Regions
{
    public class CreateRegionsRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
