using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookStore_API.Models;
using BookStoreAPI.DTOs.AdminDTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
         UserManager<IdentityUser> usermanager;
         RoleManager<IdentityRole> rolemanager;

        public AdminController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }

       
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new admin", Description = "Creates a new admin user and assigns them the 'admin' role.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error or failed creation.")]
        public IActionResult Create(AddAdminDTO ad)
        {
            Admin _admin = new Admin()
            {
                Email = ad.email,
                UserName = ad.username,
                PhoneNumber = ad.phonenumber,
            };

            IdentityResult r = usermanager.CreateAsync(_admin, ad.password).Result;
            if (r.Succeeded)
            {
                IdentityResult rr = usermanager.AddToRoleAsync(_admin, "admin").Result;

                if (rr.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(rr.Errors);
                }
            }
            else
            {
                return BadRequest(r.Errors);
            }
        }

       
        [HttpGet]
        [SwaggerOperation(Summary = "Check authentication status", Description = "Returns OK if the user is authenticated, otherwise returns Unauthorized.")]
        [SwaggerResponse(StatusCodes.Status200OK, "User is authenticated.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated.")]
        public IActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
