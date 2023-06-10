using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;

namespace PR_103_2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PR_103_2019Context _context;
        private IOrderService _orderService;

        public OrdersController(PR_103_2019Context context, IOrderService service)
        {
            _context = context;
            _orderService = service;
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrder()
        {
            return Ok(_orderService.GetAllOrders());
                
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(long id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        // PUT: api/Orders/5
        //[HttpPut("{id}")]
            

        // POST: api/Orders
        [HttpPost]
        public IActionResult CreateOrder(long userId, [FromBody] OrderDto order)
        {
            return Ok(_orderService.CreateOrder(order, userId));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            _orderService.DeleteOrder(id);
            return Ok();
        }

        private bool OrderExists(long id)
        {
            return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
