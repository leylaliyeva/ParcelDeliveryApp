using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.DTOs;
using OrderService.Enums;
using OrderService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                CreatedBy = "User.Identity.Name", // Get the user from the token
                Destination = createOrderDto.Destination,
                PickupLocation = createOrderDto.PickupLocation,
                Weight = createOrderDto.Weight,
                Status = OrderStatus.Pending
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDto = new OrderDto
            {
                Id = order.Id,
                CreatedBy = order.CreatedBy,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Destination = order.Destination,
                PickupLocation = order.PickupLocation,
                Weight = order.Weight,
                CourierId = order.CourierId
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null || order.CreatedBy != User.Identity.Name)
                return NotFound();

            var orderDto = new OrderDto
            {
                Id = order.Id,
                CreatedBy = order.CreatedBy,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Destination = order.Destination,
                PickupLocation = order.PickupLocation,
                Weight = order.Weight,
                CourierId = order.CourierId
            };

            return Ok(orderDto);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var isAdmin = User.IsInRole("Admin");
            IEnumerable<Order> orders;
            if (isAdmin)
            {
                orders = await _context.Orders
                .ToListAsync();
            }
            else
            {
                orders = await _context.Orders
                    .Where(o => o.CreatedBy == User.Identity.Name)
                    .ToListAsync();
            }
            var orderDtos = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CreatedBy = order.CreatedBy,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Destination = order.Destination,
                PickupLocation = order.PickupLocation,
                Weight = order.Weight,
                CourierId = order.CourierId
            }).ToList();
            

            return Ok(orderDtos);
        }


        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var order = await _context.Orders.FindAsync(updateOrderDto.Id);

            if (order == null || order.CreatedBy != User.Identity.Name || order.Status != OrderStatus.PickedUp)
                return NotFound();

            order.Destination = updateOrderDto.Destination ?? order.Destination;
            order.PickupLocation = updateOrderDto.PickupLocation ?? order.PickupLocation;
            if (updateOrderDto.Weight.HasValue) order.Weight = updateOrderDto.Weight.Value;
            if (updateOrderDto.Status.HasValue) order.Status = updateOrderDto.Status.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpDelete("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null || order.CreatedBy != User.Identity.Name || order.Status != OrderStatus.PickedUp)
                return NotFound();

            order.Status = OrderStatus.Canceled;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")] 
        [HttpPut("{id}/assign-courier")]
        public async Task<IActionResult> AssignCourier(int id, [FromBody] int courierId)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.CourierId = courierId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
