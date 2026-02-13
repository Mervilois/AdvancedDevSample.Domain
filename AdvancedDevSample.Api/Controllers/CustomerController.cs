using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // CREATE
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            try
            {
                var customerId = _customerService.CreateCustomer(request);
                return CreatedAtAction(nameof(GetCustomer), new { id = customerId },
                    new { id = customerId, message = "Client créé avec succès" });
            }
            catch (DomaineException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // READ ALL
        [HttpGet]
        public IActionResult GetAllCustomers([FromQuery] bool activeOnly = true)
        {
            try
            {
                var customers = activeOnly
                    ? _customerService.GetActiveCustomers()
                    : _customerService.GetAllCustomers();

                var response = customers.Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erreur interne du serveur" });
            }
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id)
        {
            try
            {
                var customer = _customerService.GetCustomer(id);
                var response = new CustomerResponse
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    FullName = customer.FullName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    IsActive = customer.IsActive,
                    CreatedAt = customer.CreatedAt
                };
                return Ok(response);
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                _customerService.UpdateCustomer(id, request);
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

        // DELETE (soft delete)
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // SEARCH
        [HttpGet("search")]
        public IActionResult SearchCustomers([FromQuery] string term)
        {
            try
            {
                var customers = _customerService.SearchCustomers(term);
                var response = customers.Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FullName = c.FullName,
                    Email = c.Email,
                    IsActive = c.IsActive
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erreur lors de la recherche" });
            }
        }

        // ACTIVATE/DEACTIVATE
        [HttpPut("{id}/activate")]
        public IActionResult ActivateCustomer(Guid id)
        {
            try
            {
                _customerService.ActivateCustomer(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/deactivate")]
        public IActionResult DeactivateCustomer(Guid id)
        {
            try
            {
                _customerService.DeactivateCustomer(id);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // COUNT
        [HttpGet("count")]
        public IActionResult GetCustomerCount()
        {
            try
            {
                var count = _customerService.GetCustomerCount();
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erreur lors du comptage" });
            }
        }
    }
}
