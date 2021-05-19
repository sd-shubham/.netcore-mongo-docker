using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoWebApiCore.Entity;
using MongoWebApiCore.Iservices;

namespace MongoWebApiCore.Controllers
{
    [Route("api/product")]
    [ApiController]

    public class WeatherForecastController : ControllerBase
    {
        private readonly IProduct _product;
        public WeatherForecastController(IProduct product)
        {
            _product = product;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Product product)
        {
            await _product.CreateAsync(product);
            return Ok("created");
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await  _product.GetAsync();
            if (!products.Any()) return NoContent();
            return Ok(products);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            var deletedRecordCount = await _product.DeleteAllAsync();
            return Ok($"{deletedRecordCount} record deleted successfully");
        }
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
           return Ok( await _product.GetbyIdAsync(id));
        }
    }
}
