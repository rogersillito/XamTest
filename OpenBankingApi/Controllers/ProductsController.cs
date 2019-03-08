using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSample.DataAccess.Models;
using WebApiSample.DataAccess.Repositories;

namespace WebApiSample.Api._22.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public ProductsController()
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            //var product = await _repository.GetProductAsync(id);

            //#region snippet_ProblemDetailsStatusCode
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //#endregion

            return new ActionResult<Product>(new Product()
            { Description = "Dog", Id = 1, IsDiscontinued = false, Name = "Bob" });
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAsync(
            [FromQuery] bool discontinuedOnly = false)
        {
            var prods = Enumerable.Range(1, 10).Select(i => new Product {Description = "Dog", Id = i, IsDiscontinued = false, Name = "Bob"}).ToList();
            return new ActionResult<List<Product>>(prods);
            //List<Product> products = null;

            //if (discontinuedOnly)
            //{
            //    products = await _repository.GetDiscontinuedProductsAsync();
            //}
            //else
            //{
            //    products = await _repository.GetProductsAsync();
            //}

            //return products;
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<Product>> CreateAsync(Product product)
        //{
        //    await _repository.AddProductAsync(product);

        //    return CreatedAtAction(nameof(GetByIdAsync),
        //        new { id = product.Id }, product);
        //}
    }

}
