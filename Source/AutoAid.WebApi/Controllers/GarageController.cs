using AutoAid.Application.Service;
using AutoAid.Domain.Dto.Garage;
using Microsoft.AspNetCore.Mvc;

namespace AutoAid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService _garageService;
        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGarage([FromBody] CreateGarageReq req)
        {
            var result = await _garageService.Create(req);
            return Ok(result);
        }
    }
}
