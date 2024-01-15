using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Difficulty
{
    public class UpdateDifficultyRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be of 3 characters at least")]
        [MaxLength(3, ErrorMessage = "Code length should not be more than 3 characters")]
        public string Name { get; set; }

    }
}
