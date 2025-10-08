using System.ComponentModel.DataAnnotations;
using RentalService.Application.Validation;

namespace RentalService.Application.DTOs;

public sealed record PickupRequest(
    [Required(ErrorMessage = "Booking number is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Booking number must be between 1 and 50 characters")]
    string BookingNumber,
    [Required(ErrorMessage = "Registration number is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Registration number must be between 1 and 20 characters")]
    string RegistrationNumber,
    [Required(ErrorMessage = "Customer ID is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer ID must be between 1 and 50 characters")]
    string CustomerId,
    [Required(ErrorMessage = "Car category is required")]
    [RegularExpression("^(SmallCar|Combi|Truck)$", ErrorMessage = "Category must be SmallCar, Combi, or Truck")]
    string Category,
    [Required(ErrorMessage = "Pickup date is required")]
    [FutureDateValidation(ErrorMessage = "Pickup date cannot be in the future")]
    DateTime PickupDate,
    [Range(0, long.MaxValue, ErrorMessage = "Pickup kilometers must be non-negative")]
    long PickupKm);