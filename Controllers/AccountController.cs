using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPI.DTOs.CustomerDTO;
using BookStoreAPI.DTOs.accountDTO;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signin;
        private readonly UserManager<IdentityUser> _manager;

        public AccountController(SignInManager<IdentityUser> _signin, UserManager<IdentityUser> _manager)
        {
            this._signin = _signin;
            this._manager = _manager;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates a user and generates a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login successful. Returns JWT token.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid username or password.")]
        public IActionResult Login(loginDTO logindata)
        {
            var r = _signin.PasswordSignInAsync(logindata.username, logindata.password, false, false).Result;
            if (r.Succeeded)
            {
                var _user = _manager.FindByNameAsync(logindata.username).Result;

                #region Claims
                List<Claim> userdata = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, _user.Id)
                };

                var roles = _manager.GetRolesAsync(_user).Result;
                foreach (var itemRole in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                }
                #endregion

                #region Secret Key
                string key = "welcome to my secret key Yasmin Gamal";
                var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                #endregion

                var signingCreds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                #region Generate Token
                var token = new JwtSecurityToken(
                    claims: userdata,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: signingCreds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                #endregion

                return Ok(tokenString);
            }

            return Unauthorized("Invalid username or password.");
        }

        
        [HttpPost("changepassword")]
        [Authorize]
        [SwaggerOperation(Summary = "Change user password", Description = "Allows an authenticated user to change their password.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to change password. Returns validation errors.")]
        public IActionResult ChangePassword(changePasswordDTO pass)
        {
            if (ModelState.IsValid)
            {
                var _cust = _manager.FindByNameAsync(User.Identity.Name).Result;
                var r = _manager.ChangePasswordAsync(_cust, pass.oldpassword, pass.newpassword).Result;
                if (r.Succeeded)
                    return Ok();
                else
                    return BadRequest(r.Errors);
            }
            return BadRequest(ModelState);
        }

        
        [HttpGet("logout")]
        [Authorize]
        [SwaggerOperation(Summary = "User logout", Description = "Logs out the authenticated user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "User logged out successfully.")]
        public IActionResult Logout()
        {
            _signin.SignOutAsync();
            return Ok();
        }
    }
}
