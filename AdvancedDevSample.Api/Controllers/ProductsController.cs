using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.DTOs;
using System;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // CREATE
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var productId = _productService.CreateProduct(
                    request.Name!,
                    request.Price,
                    request.Description
                );
                return CreatedAtAction(nameof(GetProduct), new { id = productId }, new { id = productId });
            }
            catch (ApplicationServiceException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DomaineException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // READ - Get All
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            try
            {
                return Ok(new { message = "Methode a implémenter" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Erreur interne du serveur" });
            }
        }

        // READ - Get by ID
        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                return Ok(product);
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // UPDATE 
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                _productService.UpdateProduct(id, request.Name!, request.Price, request.Description);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (DomaineException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }


        [HttpPut("{id}/price")]
        public IActionResult ChangePrice(Guid id, [FromBody] ChangePriceRequest request)
        {
            try
            {
                _productService.ChangeProductPrice(id, request.NewPrice);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DomaineException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/discount")]
        public IActionResult ApplyDiscount(Guid id, [FromBody] decimal discount)
        {
            try
            {
                _productService.ApplyProductDiscount(id, discount);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (DomaineException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/activate")]
        public IActionResult ActivateProduct(Guid id)
        {
            try
            {
                _productService.ActivateProduct(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/deactivate")]
        public IActionResult DeactivateProduct(Guid id)
        {
            try
            {
                _productService.DeactivateProduct(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
