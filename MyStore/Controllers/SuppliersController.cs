using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService supplierServices;
        public SuppliersController (ISupplierService supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        // GET: api/<SuppliersController>
        [HttpGet]
        public ActionResult<IEnumerable<SupplierModel>> Get()
        {
            var supplierList = supplierServices.GetAllSuppliers();
            return Ok(supplierList);
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public ActionResult<SupplierModel> Get(int id)
        {
            var supplier = supplierServices.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public IActionResult Post([FromBody] SupplierModel newSupplier)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var addedSupplier = supplierServices.AddSupplier(newSupplier);

            return CreatedAtAction("Get",new {id = addedSupplier.Supplierid},addedSupplier);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SupplierModel supplierToUpdate)
        {
            if(id!= supplierToUpdate.Supplierid)
            {
                return BadRequest();
            }

            if (!supplierServices.Exists(id))
            {
                return NotFound();
            }

            supplierServices.UpdateSupplier(supplierToUpdate);
            return NoContent();

        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!supplierServices.Exists(id))
            {
                return NotFound();
            }

            var isDeleted = supplierServices.DeleteSupplier(id);

            return NoContent();
        }
    }
}
