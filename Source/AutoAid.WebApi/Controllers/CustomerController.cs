using AutoAid.Application.Service;
using AutoAid.Domain.Dto.Customer;
using Microsoft.AspNetCore.Mvc;

namespace AutoAid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerReq req)
        {
            var result = await _customerService.CreateCustomer(req);
            return Ok(result);
        }
    }
}
