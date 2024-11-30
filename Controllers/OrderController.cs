using BookStoreAPI.DTOs.OrderDTO;
using BookStore_API.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        UnitOFWork _unit;
        public OrderController(UnitOFWork _unit)
        {
            this._unit = _unit;
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order")]
        [SwaggerResponse(201, "If the order is created successfully")]
        [SwaggerResponse(400, "If invalid order data is provided")]
        public IActionResult add(AddOrderDTO _order)
        {


            Order baicorderinfo = new Order()
            {
                cust_id = _order.cust_id,
                orderdate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                status = "create"

            };

            _unit.OrderRepository.add(baicorderinfo);
            _unit.savechanges();



            decimal totalprice = 0;
            foreach (var item in _order.books)
            {
                Book b = _unit.BooksRepository.selectbyid(item.book_id);
                totalprice = totalprice + (b.price * item.quentity);
                OrderDetails _details = new OrderDetails()
                {
                    order_id = baicorderinfo.id,
                    book_id = item.book_id,
                    quentity = item.quentity,
                    unitprice = b.price,
                };
                if (b.stock > _details.quentity)
                {
                    _unit.OrderDetailsRepository.add(_details);

                    b.stock -= item.quentity;
                    _unit.BooksRepository.update(b);
                }
                else
                {
                    _unit.OrderRepository.delete(baicorderinfo.id);
                    return BadRequest("invalid quantity");
                }

            }

            baicorderinfo.totalprice = totalprice;
            _unit.OrderRepository.update(baicorderinfo);

            _unit.savechanges();


            return Ok();
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get details of a specific order")]
        [SwaggerResponse(200, "If the order is found", typeof(DisplayOrderDTO))]
        [SwaggerResponse(404, "If the order is not found")]
        [SwaggerIgnore]
        public IActionResult getbyid(int id)
        {
            Order order = _unit.OrderRepository.selectbyid(id);
            if (order != null)
            {
                var response = new DisplayOrderDTO
                {
                    OrderId = order.id,
                    CustomerId = order.cust_id,
                    OrderDate = order.orderdate,
                    Books = order.OrderDetails.Select(d => new DisplayOrderDetailDTO
                    {
                        BookId = d.book_id,
                        BookTitle = d.book.title,
                        Quantity = d.quentity
                    }).ToList()
                };
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Update order and its details")]
        [SwaggerResponse(204, "If the order is updated successfully")]
        [SwaggerResponse(400, "If invalid order data is provided")]
        [SwaggerResponse(404, "If the order is not found")]
        public IActionResult edit(int id, AddOrderDTO orderdto)
        {
            if (ModelState.IsValid)
            {
                Order existingOrder = _unit.OrderRepository.selectbyid(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }

               
                existingOrder.cust_id = orderdto.cust_id;

                
                existingOrder.OrderDetails.Clear();

               
                foreach (var detailDTO in orderdto.books)
                {
                    Book book = _unit.BooksRepository.selectbyid(detailDTO.book_id);
                    if (book == null || book.stock < detailDTO.quentity)
                    {
                        return BadRequest($"Invalid quantity for book ID: {detailDTO.book_id}");
                    }

                  
                    OrderDetails newDetail = new OrderDetails
                    {
                        order_id = existingOrder.id,
                        book_id = detailDTO.book_id,
                        quentity = detailDTO.quentity,
                        unitprice = book.price
                    };
                    existingOrder.OrderDetails.Add(newDetail);

                    
                    book.stock -= detailDTO.quentity;
                    _unit.BooksRepository.update(book);
                }

                
                _unit.OrderRepository.update(existingOrder);
                _unit.savechanges();

                return NoContent();
            }
            return BadRequest(ModelState);
        }


    }
}
