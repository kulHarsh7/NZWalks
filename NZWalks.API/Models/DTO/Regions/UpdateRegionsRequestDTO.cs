using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Regions
{
    public class UpdateRegionsRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be of 3 characters at least")]
        [MaxLength(3, ErrorMessage = "Code length should not be more than 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name should not be more than 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
