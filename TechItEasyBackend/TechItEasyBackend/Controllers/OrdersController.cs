using Microsoft.AspNetCore.Mvc;
using TechItEasyBackend.Persistence;
using TechItEasyBackend.Requests;

namespace TechItEasyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly StoreRepository _repository;

        public OrdersController(StoreRepository repository)
        {
            _repository = repository;
        }

        // GET api/<OrdersController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dtos.OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orders = await _repository.GetOrders();
            var orderDtos = orders.Select(x => Dtos.OrderDto.FromModel(x));
            return Ok(orderDtos);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dtos.OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _repository.GetOrder(id);
            if (order == null)
                return NotFound();

            var orderDto = Dtos.OrderDto.FromModel(order);
            return Ok(orderDto);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task Post([FromBody] CreateOrderRequest request)
        {
            await _repository.CreateOrder(request);
        }
    }
}
