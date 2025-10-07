namespace RentalService.Application.DTOs;

public sealed record RentalResult(
    string BookingNumber,
    decimal Price,
    int Days,
    long Km);