using BookStore_API.DTOs.AuthorsDTO;
using BookStore_API.Models;

using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin,customer")]
    public class AuthorsController : ControllerBase
    {
        private readonly UnitOFWork _unit;

        public AuthorsController(UnitOFWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Fetch all authors", Description = "Retrieve a list of all authors.")]
        [SwaggerResponse(200, "Returns a list of all authors", typeof(List<DisplayAuthorDTO>))]
        public IActionResult GetAll()
        {
            List<Author> authors = _unit.AuthorsRepository.selectall();
            List<DisplayAuthorDTO> authorDTOs = authors.Select(a => new DisplayAuthorDTO
            {
                Id = a.id,
                Name = a.name,
                Bio = a.bio,
                Age = a.age,
                NumberOfBooks = a.numberOfBooks
            }).ToList();

            return Ok(authorDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Fetch a specific author", Description = "Retrieve details of a specific author by ID.")]
        [SwaggerResponse(200, "Returns the author details", typeof(DisplayAuthorDTO))]
        [SwaggerResponse(404, "If the author is not found")]
        public IActionResult GetById(int id)
        {
            Author author = _unit.AuthorsRepository.selectbyid(id);
            if (author == null)
            {
                return NotFound();
            }

            DisplayAuthorDTO authorDTO = new DisplayAuthorDTO
            {
                Id = author.id,
                Name = author.name,
                Bio = author.bio,
                Age = author.age,
                NumberOfBooks = author.numberOfBooks
            };

            return Ok(authorDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Add a new author", Description = "Admin access required.")]
        [SwaggerResponse(201, "If the author is created successfully")]
        [SwaggerResponse(400, "If the author data is invalid")]
        public IActionResult Add(AddAuthorDTO authorDTO)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author
                {
                    name = authorDTO.Name,
                    bio = authorDTO.Bio,
                    age = authorDTO.Age,
                    numberOfBooks = 0 // Initialize as 0
                };

                _unit.AuthorsRepository.add(author);
                _unit.savechanges();
                return CreatedAtAction(nameof(GetById), new { id = author.id }, null);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Update author information", Description = "Admin access required.")]
        [SwaggerResponse(204, "If the author is updated successfully")]
        [SwaggerResponse(400, "If the author data is invalid")]
        public IActionResult Edit(int id, AddAuthorDTO authorDTO)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author
                {
                    id = id,
                    name = authorDTO.Name,
                    bio = authorDTO.Bio,
                    age = authorDTO.Age
                };

                _unit.AuthorsRepository.update(author);
                _unit.savechanges();
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Delete an author", Description = "Admin access required.")]
        [SwaggerResponse(200, "If the author is deleted successfully")]
        public IActionResult Delete(int id)
        {
            _unit.AuthorsRepository.delete(id);
            _unit.savechanges();
            return Ok();
        }
    }
}
