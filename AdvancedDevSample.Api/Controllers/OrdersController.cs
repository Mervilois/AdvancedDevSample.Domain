using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var orderId = _orderService.CreateOrder(request);
                var order = _orderService.GetOrder(orderId);

                return CreatedAtAction(nameof(GetOrder), new { id = orderId }, new
                {
                    order.Id,
                    order.OrderNumber,
                    order.TotalAmount,
                    Status = order.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
            => Ok(_orderService.GetAllOrders());

        [HttpGet("{id}")]
        public IActionResult GetOrder(Guid id)
        {
            try
            {
                var order = _orderService.GetOrder(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetCustomerOrders(Guid customerId)
        {
            var orders = _orderService.GetCustomerOrders(customerId);
            return Ok(orders);
        }

        [HttpPut("{id}/confirm")]
        public IActionResult ConfirmOrder(Guid id)
        {
            try
            {
                _orderService.ConfirmOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/cancel")]
        public IActionResult CancelOrder(Guid id)
        {
            try
            {
                _orderService.CancelOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
