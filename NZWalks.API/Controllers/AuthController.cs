using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Auth;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> usermanager;

        public AuthController(UserManager<IdentityUser> usermanager)
        {
            this.usermanager = usermanager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser()
                {
                    UserName = registerUserDTO.UserName,
                    Email = registerUserDTO.UserName
                };
                var identityUserdata = await usermanager.CreateAsync(identityUser, registerUserDTO.Password);

                if (identityUserdata.Succeeded)
                {
                    if (registerUserDTO.Roles != null && registerUserDTO.Roles.Any())
                    {
                        identityUserdata = await usermanager.AddToRolesAsync(identityUser, registerUserDTO.Roles);
                        if (identityUserdata.Succeeded)
                        {
                            return Ok("User Created");
                        }
                    }

                }
                return BadRequest("Something went wrong");
               
            }
            return BadRequest("data is not valid");
        }
    }
}
