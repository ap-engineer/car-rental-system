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
        if (PickupDate.HasValue)
            throw new InvalidOperationException("Pickup already registered.");

        PickupDate = date;
        PickupKm = km;
    }

    public void RegisterReturn(DateTime date, long km)
    {
        if (!PickupDate.HasValue)
            throw new InvalidOperationException("Pickup must be registered first.");

        if (ReturnDate.HasValue)
            throw new InvalidOperationException("Return already registered.");

        ReturnDate = date;
        ReturnKm = km;
    }

    public int NumberOfDays => (PickupDate, ReturnDate) switch
    {
        (not null, not null) => Math.Max(1, (int)Math.Ceiling((ReturnDate.Value - PickupDate.Value).TotalDays)),
        _ => 0
    };

    public long KilometersDriven => (PickupKm, ReturnKm) switch
    {
        (not null, not null) => Math.Max(0, ReturnKm.Value - PickupKm.Value),
        _ => 0
    };
}