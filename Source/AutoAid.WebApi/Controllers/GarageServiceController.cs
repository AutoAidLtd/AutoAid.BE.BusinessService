using AutoAid.Application.Service;
using AutoAid.Domain.Dto.GarageService;
using Microsoft.AspNetCore.Mvc;

namespace AutoAid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageServiceController : ControllerBase
    {
        private readonly IGarageServiceService _garageServiceService;

        public GarageServiceController(IGarageServiceService garageServiceService)
        {
            _garageServiceService = garageServiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGarageService(CreateGarageServiceReq req)
        {
            var result = await _garageServiceService.CreateAGarageService(req);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
