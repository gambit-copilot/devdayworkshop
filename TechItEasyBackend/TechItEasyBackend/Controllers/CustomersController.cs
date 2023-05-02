using Microsoft.AspNetCore.Mvc;
using TechItEasyBackend.Persistence;
using TechItEasyBackend.Requests;

namespace TechItEasyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly StoreRepository _repository;

        public CustomersController(StoreRepository repository)
        {
            _repository = repository;
        }

        // GET api/<CustomersController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dtos.Customer>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var customers = await _repository.GetCustomers();
            var customerDtos = customers.Select(x => Dtos.Customer.FromModel(x));
            return Ok(customerDtos);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dtos.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _repository.GetCustomer(id);
            if (customer == null)
                return NotFound();

            var customerDto = Dtos.Customer.FromModel(customer);
            return Ok(customerDto);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task Post([FromBody] CreateCustomerRequest request)
        {
            await _repository.CreateCustomer(request);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CreateCustomerRequest request)
        {
            await _repository.UpdateCustomer(id, request);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteCustomer(id);
        }
    }
}
