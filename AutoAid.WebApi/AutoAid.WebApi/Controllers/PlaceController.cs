using AutoAid.Application.Service;
using AutoAid.Domain.Dto.Place;
using Microsoft.AspNetCore.Mvc;

namespace AutoAid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<IActionResult> CreatePlace(CreatePlaceDto createData)
        {
            var result = await _placeService.Create(createData);
            return Ok(result);
        }
    }
}
