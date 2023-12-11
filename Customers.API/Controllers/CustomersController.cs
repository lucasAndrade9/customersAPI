using Customers.API.Model;
using Customers.API.Models;
using Customers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        [HttpPost] 
        public IActionResult CreateRange([FromBody] CustomerInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                    .SelectMany(modelState => modelState.Value!.Errors)
                    .Select(err => err.ErrorMessage)
                    .ToList();

                return BadRequest(messages);
            }

            _customerService.Create(inputModel.Customers);

            return CreatedAtAction(nameof(Customers), inputModel);
        }
    }
}
