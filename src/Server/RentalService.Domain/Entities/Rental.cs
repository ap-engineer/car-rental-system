namespace RentalService.Domain.Entities;

public sealed class Rental
{
    public string BookingNumber { get; }
    public string RegistrationNumber { get; }
    public string CustomerId { get; }
    public CarCategory Category { get; }

    public DateTime? PickupDate { get; private set; }
    public long? PickupKm { get; private set; }

    public DateTime? ReturnDate { get; private set; }
    public long? ReturnKm { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public Rental(string bookingNumber, string registrationNumber, string customerId, CarCategory category)
    {
        BookingNumber = bookingNumber;
        RegistrationNumber = registrationNumber;
        CustomerId = customerId;
        Category = category;
    }

    public void RegisterPickup(DateTime date, long km)
    {
        PickupDate = date;
        PickupKm = km;
    }

    public void RegisterReturn(DateTime date, long km)
    {
        ReturnDate = date;
        ReturnKm = km;
    }

    public int NumberOfDays => ReturnDate.HasValue && PickupDate.HasValue
        ? (int)Math.Ceiling((ReturnDate.Value - PickupDate.Value).TotalDays)
        : 0;

    public long KilometersDriven => ReturnKm.HasValue && PickupKm.HasValue
        ? ReturnKm.Value - PickupKm.Value
        : 0;
}