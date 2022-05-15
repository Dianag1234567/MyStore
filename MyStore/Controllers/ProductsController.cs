using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            var productList = productService.GetAllProducts();

            return Ok(productList);
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<ProductModel>> Get(int id)
        //{
        //    var productList = productService.GetAllProducts();

        //    return Ok(productList);
        //}
        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        //public ProductModel Get(int id)
        //{
        //    var product = productService.GetById(id);

        //    return product;
        //}
        public ActionResult<ProductModel> GetProduct(int id)
        {
            var result = productService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult<ProductModel> Post([FromBody] ProductModel newProduct)
        {
            //fail fast -> return
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedProduct = productService.AddProduct(newProduct);
            return CreatedAtAction("Get", new { id = addedProduct.Productid }, addedProduct);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ProductModel))]
        //[Consumes(MediaTypeNames.Text.Html)]
        public IActionResult Put(int id, [FromBody] ProductModel productToUpdate)
        {
            //exists by id
            if (id != productToUpdate.Productid)
            {
                return BadRequest();
            }

            //var 1:
            if (!productService.Exists(id))//nu exista -> Not Found()
            {
                return NotFound();
            }

            productService.UpdateProduct(productToUpdate);
            return NoContent();
             
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!productService.Exists(id))
            {
                return NotFound();
            }

            var isDeleted = productService.DeleteProduct(id);

            return NoContent();

        }
    }
}
