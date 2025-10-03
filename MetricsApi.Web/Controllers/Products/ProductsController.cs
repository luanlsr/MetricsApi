using MediatR;
using MetricsApi.Application.Commands.Products;
using MetricsApi.Application.Queries.Products;
using MetricsApi.CrossCutting.Dtos.Producs;
using Microsoft.AspNetCore.Mvc;

namespace MetricsApi.Web.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest("Id mismatch");
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpPatch("{id:guid}/price")]
        public async Task<ActionResult<ProductDto>> UpdatePrice(Guid id, [FromBody] UpdateProductPriceCommand command)
        {
            if (id != command.Id) return BadRequest("Id mismatch");
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
