using Microsoft.AspNetCore.Mvc;
using TechItEasyBackend.Persistence;
using TechItEasyBackend.Requests;

namespace TechItEasyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreRepository _repository;

        public ProductsController(StoreRepository repository)
        {
            _repository = repository;
        }

        // GET api/<ProductsController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dtos.Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.GetProducts();
            var productDtos = products.Select(x => Dtos.Product.FromModel(x));
            return Ok(productDtos);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dtos.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
                return NotFound();

            var productDto = Dtos.Product.FromModel(product);
            return Ok(productDto);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] CreateProductRequest request)
        {
            await _repository.CreateProduct(request);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CreateProductRequest request)
        {
            await _repository.UpdateProduct(id, request);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteProduct(id);
        }
    }
}
