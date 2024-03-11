using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Auth
{
    public class RegisterUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
