using Microsoft.Extensions.Options;
using RentalService.Application.DTOs;
using RentalService.Domain.Configuration;
using RentalService.Domain.Entities;
using RentalService.Domain.Services.Implementation;
using Xunit;

namespace RentalService.Tests;

public class ValidationTests
{
    private readonly Application.Services.Implementation.RentalService _service;
    private readonly RentalPricingOptions _pricingOptions = new() { BaseDayRental = 100, BaseKmPrice = 2 };

    // public ValidationTests()
    // {
    //     var options = Options.Create(_pricingOptions);
    //     var pricingService = new RentalPricingService(options);
    //     _service = new Application.Services.Implementation.RentalService(pricingService);
    // }
    //
    // [Fact]
    // public void Rental_RegisterPickup_WithFutureDate_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     var futureDate = DateTime.Now.AddDays(1);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterPickup(futureDate, 10000));
    //
    //     Assert.Contains("Pickup date cannot be in the future", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_RegisterPickup_WithNegativeKm_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterPickup(DateTime.Today, -100));
    //
    //     Assert.Contains("Pickup kilometers cannot be negative", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_RegisterReturn_WithFutureDate_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     rental.RegisterPickup(DateTime.Today, 10000);
    //     var futureDate = DateTime.Now.AddDays(1);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterReturn(futureDate, 10100));
    //
    //     Assert.Contains("Return date cannot be in the future", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_RegisterReturn_WithDateBeforePickup_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     var pickupDate = DateTime.Today;
    //     rental.RegisterPickup(pickupDate, 10000);
    //     var returnDate = pickupDate.AddDays(-1);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterReturn(returnDate, 10100));
    //
    //     Assert.Contains("Return date cannot be before pickup date", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_RegisterReturn_WithNegativeKm_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     rental.RegisterPickup(DateTime.Today, 10000);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterReturn(DateTime.Today.AddDays(1), -100));
    //
    //     Assert.Contains("Return kilometers cannot be negative", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_RegisterReturn_WithKmLessThanPickup_ThrowsException()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     rental.RegisterPickup(DateTime.Today, 10000);
    //
    //     var exception = Assert.Throws<ArgumentException>(() =>
    //         rental.RegisterReturn(DateTime.Today.AddDays(1), 9999));
    //
    //     Assert.Contains("Return kilometers cannot be less than pickup kilometers", exception.Message);
    // }
    //
    // [Fact]
    // public void Rental_NumberOfDays_SameDayRental_ReturnsOne()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     var date = DateTime.Today;
    //     rental.RegisterPickup(date, 10000);
    //     rental.RegisterReturn(date, 10100);
    //
    //     Assert.Equal(1, rental.NumberOfDays);
    // }
    //
    // [Fact]
    // public void Rental_KilometersDriven_CalculatesCorrectly()
    // {
    //     var rental = new Rental("B001", "ABC123", "1234567890", CarCategory.SmallCar);
    //     rental.RegisterPickup(DateTime.Today, 10000);
    //     rental.RegisterReturn(DateTime.Today.AddDays(1), 10250);
    //
    //     Assert.Equal(250, rental.KilometersDriven);
    // }
    //
    // [Fact]
    // public void PricingService_WithCustomConfiguration_CalculatesCorrectly()
    // {
    //     var customOptions = new RentalPricingOptions { BaseDayRental = 150, BaseKmPrice = 3 };
    //     var options = Options.Create(customOptions);
    //     var pricingService = new RentalPricingService(options);
    //
    //     var smallCarPrice = pricingService.Calculate(CarCategory.SmallCar, 2, 100);
    //     var combiPrice = pricingService.Calculate(CarCategory.Combi, 2, 100);
    //     var truckPrice = pricingService.Calculate(CarCategory.Truck, 2, 100);
    //
    //     Assert.Equal(300, smallCarPrice); // 150 * 2
    //     Assert.Equal(690, combiPrice); // 150 * 2 * 1.3 + 3 * 100
    //     Assert.Equal(900, truckPrice); // 150 * 2 * 1.5 + 3 * 100 * 1.5
    // }
}
