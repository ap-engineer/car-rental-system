using Microsoft.Extensions.Options;
using RentalService.Application.DTOs;
using RentalService.Domain.Configuration;
using RentalService.Domain.Services.Implementation;
using Xunit;

namespace RentalService.Tests;

public class RentalServiceTests
{
    private readonly Application.Services.Implementation.RentalService _service;
    private readonly RentalPricingOptions _pricingOptions = new() { BaseDayRental = 100, BaseKmPrice = 2 };

    // public RentalServiceTests()
    // {
    //     var options = Options.Create(_pricingOptions);
    //     var pricingService = new RentalPricingService(options);
    //     _service = new Application.Services.Implementation.RentalService(pricingService);
    // }
    //
    // [Fact]
    // public void SmallCar_PriceCalculation_IsCorrect()
    // {
    //     _service.RegisterPickup(
    //         new PickupRequest(
    //             "B001",
    //             "ABC123",
    //             "1234567890",
    //             "SmallCar",
    //             DateTime.Today.AddDays(-10),
    //             10000
    //         ));
    //     var result = _service.RegisterReturn(new ReturnRequest("B001", DateTime.Today.AddDays(-1), 10000));
    //
    //     Assert.Equal(900, result.Price); // baseDayRental=100 * 9 days
    // }
    //
    // [Fact]
    // public void Combi_PriceCalculation_IsCorrect()
    // {
    //     _service.RegisterPickup(new PickupRequest("B002", "DEF456", "2345678901", "Combi", DateTime.Today, 10000));
    //     var result = _service.RegisterReturn(new ReturnRequest("B002", DateTime.Today.AddDays(2), 10150));
    //
    //     var expectedPrice = 100 * 2 * 1.3m + 2 * 150; // (baseDayRental * days * 1.3) + (baseKmPrice * km)
    //     Assert.Equal(expectedPrice, result.Price);
    // }
    //
    // [Fact]
    // public void Truck_PriceCalculation_IsCorrect()
    // {
    //     _service.RegisterPickup(new PickupRequest("B003", "XYZ789", "9876543210", "Truck", DateTime.Today, 10000));
    //     var result = _service.RegisterReturn(new ReturnRequest("B003", DateTime.Today.AddDays(2), 10200));
    //
    //     Assert.Equal(100 * 2 * 1.5m + 2 * 200 * 1.5m, result.Price);
    // }
    //
    // [Fact]
    // public void RegisterReturn_WithoutPickup_ThrowsException()
    // {
    //     var exception = Assert.Throws<KeyNotFoundException>(() =>
    //         _service.RegisterReturn(new ReturnRequest("B999", DateTime.Today, 10000)));
    //
    //     Assert.Contains("Booking B999 not found", exception.Message);
    // }
    //
    // [Fact]
    // public void RegisterPickup_DuplicateBooking_ThrowsException()
    // {
    //     _service.RegisterPickup(new PickupRequest("B004", "ABC123", "1234567890", "SmallCar", DateTime.Today, 10000));
    //
    //     var exception = Assert.Throws<InvalidOperationException>(() =>
    //         _service.RegisterPickup(new PickupRequest("B004", "DEF456", "2345678901", "Combi", DateTime.Today, 10000)));
    //
    //     Assert.Contains("Pickup already registered", exception.Message);
    // }
    //
    // [Fact]
    // public void RegisterReturn_DuplicateReturn_ThrowsException()
    // {
    //     _service.RegisterPickup(new PickupRequest("B005", "ABC123", "1234567890", "SmallCar", DateTime.Today, 10000));
    //     _service.RegisterReturn(new ReturnRequest("B005", DateTime.Today.AddDays(1), 10100));
    //
    //     var exception = Assert.Throws<InvalidOperationException>(() =>
    //         _service.RegisterReturn(new ReturnRequest("B005", DateTime.Today.AddDays(2), 10200)));
    //
    //     Assert.Contains("Return already registered", exception.Message);
    // }
}