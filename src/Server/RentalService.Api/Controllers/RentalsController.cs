using Microsoft.AspNetCore.Mvc;
using RentalService.Application.DTOs;
using RentalService.Application.Services.Interfaces;

namespace RentalService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RentalsController(IRentalService service) : ControllerBase
{
    [HttpPost("pickup")]
    public IActionResult RegisterPickup([FromBody] PickupRequest request)
    {
        service.RegisterPickup(request);
        return Ok(new { message = "Pickup registered successfully" });
    }

    [HttpPost("return")]
    public ActionResult<RentalResult> RegisterReturn([FromBody] ReturnRequest request)
    {
        var result = service.RegisterReturn(request);
        return Ok(result);
    }
}