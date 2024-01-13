namespace NZWalks.API.Models.DTO.Walks
{
    public class CreateWalksRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double lengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
