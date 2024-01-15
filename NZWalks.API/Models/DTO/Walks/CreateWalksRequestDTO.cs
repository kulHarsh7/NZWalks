using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Walks
{
    public class CreateWalksRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name should not be more than 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage = "Description should not be more than 300 characters")]
        public string Description { get; set; }
        [Required]
        [Range(5,50)]
        public double lengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
