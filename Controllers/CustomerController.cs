using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookStore_API.Models;
using Microsoft.AspNetCore.Authorization;
using BookStoreAPI.DTOs.CustomerDTO;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController : ControllerBase
    {
         UserManager<IdentityUser> usermanager;
         RoleManager<IdentityRole> rolemanager;

        public CustomerController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }

       
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new customer", Description = "Creates a customer and assigns them to the 'customer' role.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Customer created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error or failed creation.")]
        public IActionResult Create([FromBody] addcusomerDTO ct)
        {
            Customer cust = new Customer()
            {
                Email = ct.email,
                UserName = ct.username,
                fullname = ct.fullname,
                address = ct.address,
                PhoneNumber = ct.phonenumber,
            };

            IdentityResult r = usermanager.CreateAsync(cust, ct.password).Result;
            if (r.Succeeded)
            {
                IdentityResult rr = usermanager.AddToRoleAsync(cust, "customer").Result;
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

      
        [HttpPut]
        [SwaggerOperation(Summary = "Edit customer profile", Description = "Updates an existing customer's profile.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Profile updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error or failed update.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
        public IActionResult EditProfile([FromBody] EditcustomerDTO _customer)
        {
            if (ModelState.IsValid)
            {
                Customer _cust = (Customer)usermanager.FindByIdAsync(_customer.id).Result;
                if (_cust == null) return NotFound();

                _cust.fullname = _customer.fullname;
                _cust.address = _customer.address;
                _cust.PhoneNumber = _customer.phonenumber;
                _cust.UserName = _customer.username;
                _cust.Email = _customer.email;

                var r = usermanager.UpdateAsync(_cust).Result;

                if (r.Succeeded)
                    return NoContent();
                else
                    return BadRequest(r.Errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

      
        [HttpPost("changepassword")]
        [SwaggerOperation(Summary = "Change customer password", Description = "Changes a customer's password.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error or failed password change.")]
        public IActionResult ChangePassword([FromBody] changePasswordDTO pass)
        {
            if (ModelState.IsValid)
            {
                Customer _cust = (Customer)usermanager.FindByIdAsync(pass.id).Result;
                var r = usermanager.ChangePasswordAsync(_cust, pass.oldpassword, pass.newpassword).Result;
                if (r.Succeeded)
                    return Ok();
                else
                    return BadRequest(r.Errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
        [HttpGet]
        [Authorize(Roles = "admin,customer")]
        [SwaggerOperation(Summary = "Retrieve all customers", Description = "Gets a list of all registered customers.")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of customers returned.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No customers found.")]
        public IActionResult GetAll()
        {
            var users = usermanager.GetUsersInRoleAsync("customer").Result.OfType<Customer>().ToList();
            if (!users.Any()) return NotFound();

            List<SelectCustomerDTO> custDTO = new List<SelectCustomerDTO>();
            foreach (var user in users)
            {
                SelectCustomerDTO cDTO = new SelectCustomerDTO()
                {
                    id = user.Id,
                    fullname = user.fullname,
                    address = user.address,
                    username = user.UserName,
                    email = user.Email,
                    phonenumber = user.PhoneNumber
                };
                custDTO.Add(cDTO);
            }
            return Ok(custDTO);
        }

        
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieve customer by ID", Description = "Gets details of a customer by their ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Customer details returned.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
        public IActionResult GetById(string id)
        {
            var cu = (Customer)usermanager.GetUsersInRoleAsync("customer").Result.Where(n => n.Id == id).FirstOrDefault();
            if (cu == null) return NotFound();

            SelectCustomerDTO custdto = new SelectCustomerDTO()
            {
                address = cu.address,
                fullname = cu.fullname,
                email = cu.Email,
                phonenumber = cu.PhoneNumber,
                id = cu.Id,
                username = cu.UserName
            };

            return Ok(custdto);
        }
    }
}
