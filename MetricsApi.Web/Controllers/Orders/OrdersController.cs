using MediatR;
using MetricsApi.Application.Commands.Orders;
using MetricsApi.Application.Queries.Orders;
using MetricsApi.CrossCutting.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

namespace MetricsApi.Web.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPost("{orderId:guid}/items")]
        public async Task<ActionResult<OrderDto>> AddItem(Guid orderId, [FromBody] AddOrderItemCommand command)
        {
            if (orderId != command.OrderId) return BadRequest("OrderId mismatch");
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpPut("{id:guid}/status")]
        public async Task<ActionResult<OrderDto>> ChangeStatus(Guid id, [FromBody] ChangeOrderStatusCommand command)
        {
            if (id != command.OrderId) return BadRequest("Id mismatch");
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }
    }
}
