namespace RentalService.Application.DTOs;

public sealed record PickupRequest(
    string BookingNumber,
    string RegistrationNumber,
    string CustomerId,
    string Category,
    DateTime PickupDate,
    long PickupKm);