namespace RentalService.Domain.Entities;

public sealed class Rental(string bookingNumber, string registrationNumber, string customerId, CarCategory category)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string BookingNumber { get; } = bookingNumber;
    public string RegistrationNumber { get; } = registrationNumber;
    public string CustomerId { get; } = customerId;
    public CarCategory Category { get; } = category;

    public DateTime? PickupDate { get; private set; }
    public long? PickupKm { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public long? ReturnKm { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public void RegisterPickup(DateTime date, long km)
    {
        if (PickupDate.HasValue)
            throw new InvalidOperationException("Pickup already registered.");

        if (date > DateTime.Now)
            throw new ArgumentException("Pickup date cannot be in the future.");

        if (km < 0)
            throw new ArgumentException("Pickup kilometers cannot be negative.");

        PickupDate = date;
        PickupKm = km;
    }

    public void RegisterReturn(DateTime date, long km)
    {
        if (!PickupDate.HasValue)
            throw new InvalidOperationException("Pickup must be registered first.");

        if (ReturnDate.HasValue)
            throw new InvalidOperationException("Return already registered.");

        if (date > DateTime.Now)
            throw new ArgumentException("Return date cannot be in the future.");

        if (date < PickupDate.Value)
            throw new ArgumentException("Return date cannot be before pickup date.");

        if (km < 0)
            throw new ArgumentException("Return kilometers cannot be negative.");

        if (km < PickupKm)
            throw new ArgumentException("Return kilometers cannot be less than pickup kilometers.");

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