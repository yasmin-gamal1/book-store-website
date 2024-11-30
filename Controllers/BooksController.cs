using BookStore_API.Models;
using BookStoreAPI.DTOs.BooksDTO;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin,customer")]

    public class BooksController : ControllerBase
    {
        UnitOFWork _unit;

        public BooksController(UnitOFWork _unit)
        {
            this._unit = _unit;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "select all books ", Description = "example:  http:/localhost/api/books")]
        [SwaggerResponse(200, "return all books", typeof(List<DisplayBookDTO>))]

        public IActionResult getall()
        {
            List<Book> books = _unit.BooksRepository.selectall();
            List<DisplayBookDTO> booksDTO = new List<DisplayBookDTO>();
            foreach (Book b in books)
            {
                DisplayBookDTO bookDTO = new DisplayBookDTO()
                {
                    id = b.id,
                    title = b.title,
                    price = b.price,
                    publishdate = b.publishdate,
                    stock = b.stock,
                    catalog = b.Catlog.name,
                    authorname = b.author.name,
                };
                booksDTO.Add(bookDTO);

            }

            return Ok(booksDTO);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "can earch on book by book id ", Description = "example:  http:/localhost/api/books")]
        [SwaggerResponse(200, "return book data", typeof(DisplayBookDTO))]
        [SwaggerResponse(404, "if no book founded")]
        [SwaggerIgnore]
        public IActionResult getbyid(int id)
        {
            Book b = _unit.BooksRepository.selectbyid(id);
            if (b != null)
            {
                DisplayBookDTO bDTO = new DisplayBookDTO()
                {
                    id = b.id,
                    title = b.title,
                    price = b.price,
                    publishdate = b.publishdate,
                    stock = b.stock,
                    catalog = b.Catlog.name,
                    authorname = b.author.name,
                };
                return Ok(bDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "add new book")]
        [SwaggerResponse(201, "if book created succcesfully")]
        [SwaggerResponse(400, "ifinvalid book data")]
        public IActionResult add(AddBookDTO bookdto)
        {
            if (ModelState.IsValid)
            {
                Book b = new Book()
                {
                    title = bookdto.title,
                    stock = bookdto.stock,
                    publishdate = bookdto.publishdate,
                    price = bookdto.price,
                    author_id = bookdto.author_id,
                    cat_id = bookdto.cat_id,

                };

                _unit.BooksRepository.add(b);
                _unit.savechanges();
                return CreatedAtAction("getbyid", new { id = b.id }, null);

            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "edit book data")]
        [SwaggerResponse(204, "if book updated succcesfully")]
        [SwaggerResponse(400, "ifinvalid book data")]
        public IActionResult edit(int id, AddBookDTO bookdto)
        {

            if (ModelState.IsValid)
            {

                Book b = new Book()
                {
                    id = id,
                    title = bookdto.title,
                    stock = bookdto.stock,
                    publishdate = bookdto.publishdate,
                    price = bookdto.price,
                    author_id = bookdto.author_id,
                    cat_id = bookdto.cat_id,

                };



                _unit.BooksRepository.update(b);
                _unit.savechanges();
                return NoContent();



            }
            return BadRequest(ModelState);

        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "delete book from datatbase")]
        [SwaggerResponse(200, "if book deleted succcesfully")]
        public IActionResult delete(int id)
        {


            _unit.BooksRepository.delete(id);
            _unit.savechanges();
            return Ok();

        }
    }
}
