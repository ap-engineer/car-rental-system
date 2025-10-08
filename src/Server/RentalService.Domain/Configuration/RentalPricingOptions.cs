namespace RentalService.Domain.Configuration;

public class RentalPricingOptions
{
    public const string SectionName = "RentalPricing";
    
    public decimal BaseDayRental { get; init; } = 100;
    public decimal BaseKmPrice { get; init; } = 2;
}
