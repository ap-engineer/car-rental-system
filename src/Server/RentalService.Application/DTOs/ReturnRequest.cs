namespace RentalService.Application.DTOs;

public sealed record ReturnRequest(
    string BookingNumber,
    DateTime ReturnDate,
    long ReturnKm);