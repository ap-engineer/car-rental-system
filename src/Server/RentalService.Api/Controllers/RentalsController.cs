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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            service.RegisterPickup(request);
            return Ok(new { message = "Pickup registered successfully" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpPost("return")]
    public ActionResult<RentalResult> RegisterReturn([FromBody] ReturnRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = service.RegisterReturn(request);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}