using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SuppliersController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSupplierRequest request)
        {
            try
            {
                var id = _supplierService.CreateSupplier(request);
                return CreatedAtAction(nameof(Get), new { id }, new { id });
            }
            catch (DomaineException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_supplierService.GetAllSuppliers());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var supplier = _supplierService.GetSupplier(id);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateSupplierRequest request)
        {
            try
            {
                _supplierService.UpdateSupplier(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _supplierService.DeleteSupplier(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string term)
        {
            var results = _supplierService.SearchSuppliers(term);
            return Ok(results);
        }
    }
}
