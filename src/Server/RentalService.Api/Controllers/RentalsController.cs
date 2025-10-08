using Microsoft.AspNetCore.Mvc;
using RentalService.Application.DTOs;
using RentalService.Application.Services.Interfaces;

namespace RentalService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RentalsController(IRentalService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RentalResult>>> GetAll()
    {
        var rentals = await service.GetAllRentals();
        return Ok(rentals);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RentalResult>> GetById(Guid id)
    {
        var rental = await service.GetRentalById(id);
        if (rental == null)
        {
            return NotFound(new { error = $"Rental with ID {id} not found." });
        }
        return Ok(rental);
    }

    [HttpPost("pickup")]
    public async Task<IActionResult> RegisterPickup([FromBody] PickupRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await service.RegisterPickup(request);
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
    public async Task<ActionResult<RentalResult>> RegisterReturn([FromBody] ReturnRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await service.RegisterReturn(request);
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