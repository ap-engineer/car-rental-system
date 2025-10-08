namespace RentalService.Application.DTOs;

public sealed record RentalResult(
    Guid Id,
    string BookingNumber,
    decimal Price,
    int Days,
    long? Km,
    bool IsReturned,
    DateTime? PickupDate);