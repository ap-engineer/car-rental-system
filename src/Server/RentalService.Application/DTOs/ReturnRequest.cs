using System.ComponentModel.DataAnnotations;
using RentalService.Application.Validation;

namespace RentalService.Application.DTOs;

public sealed record ReturnRequest(
    [Required(ErrorMessage = "Booking number is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Booking number must be between 1 and 50 characters")]
    string BookingNumber,
    
    [Required(ErrorMessage = "Return date is required")]
    [FutureDateValidation(ErrorMessage = "Return date cannot be in the future")]
    DateTime ReturnDate,
    
    [Range(0, long.MaxValue, ErrorMessage = "Return kilometers must be non-negative")]
    long ReturnKm);