using Microsoft.Extensions.Options;
using RentalService.Application.DTOs;
using RentalService.Domain.Configuration;
using RentalService.Domain.Services.Implementation;
using Xunit;

namespace RentalService.Tests;

public class RentalServiceTests
{
    private readonly Application.Services.Implementation.RentalService _service;
    private readonly InMemoryRentalRepository _repository;
    private readonly RentalPricingOptions _pricingOptions = new() { BaseDayRental = 100, BaseKmPrice = 2 };

    public RentalServiceTests()
    {
        var options = Options.Create(_pricingOptions);
        var pricingService = new RentalPricingService(options);
        _repository = new InMemoryRentalRepository();
        _service = new Application.Services.Implementation.RentalService(pricingService, _repository);
    }

    private void ClearRepository()
    {
        _repository.Clear();
    }

    [Fact]
    public async Task RegisterPickup_ValidRequest_ReturnsRentalResult()
    {
        // Arrange
        ClearRepository();
        var request = new PickupRequest(
            "B001",
            "ABC123",
            "1234567890",
            "SmallCar",
            DateTime.Today.AddDays(-1),
            10000
        );

        // Act
        var result = await _service.RegisterPickup(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("B001", result.BookingNumber);
        Assert.Equal(0, result.Price); // No price until returned
        Assert.Equal(0, result.Days);
        Assert.Equal(10000, result.Km);
        Assert.False(result.IsReturned);
        Assert.Equal(DateTime.Today.AddDays(-1), result.PickupDate);
    }

    [Fact]
    public async Task RegisterPickup_DuplicateBookingNumber_ThrowsInvalidOperationException()
    {
        // Arrange
        ClearRepository();
        var request1 = new PickupRequest("B002", "ABC123", "1234567890", "SmallCar", DateTime.Today, 10000);
        var request2 = new PickupRequest("B002", "DEF456", "0987654321", "Combi", DateTime.Today, 15000);

        // Act
        await _service.RegisterPickup(request1);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.RegisterPickup(request2));
        
        Assert.Contains("Booking number B002 already exists", exception.Message);
    }

    [Fact]
    public async Task RegisterReturn_ValidRequest_ReturnsRentalResultWithPrice()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B003", "ABC123", "1234567890", "SmallCar", DateTime.Today.AddDays(-5), 10000);
        var returnRequest = new ReturnRequest("B003", DateTime.Today.AddDays(-1), 10200);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("B003", result.BookingNumber);
        Assert.Equal(400, result.Price); // 100 * 4 days for SmallCar
        Assert.Equal(4, result.Days);
        Assert.Equal(200, result.Km);
        Assert.True(result.IsReturned);
    }

    [Fact]
    public async Task RegisterReturn_NonExistentBooking_ThrowsKeyNotFoundException()
    {
        // Arrange
        ClearRepository();
        var returnRequest = new ReturnRequest("B999", DateTime.Today, 10000);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.RegisterReturn(returnRequest));
        
        Assert.Contains("Booking B999 not found", exception.Message);
    }

    [Fact]
    public async Task SmallCar_PriceCalculation_IsCorrect()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B004", "ABC123", "1234567890", "SmallCar", DateTime.Today.AddDays(-10), 10000);
        var returnRequest = new ReturnRequest("B004", DateTime.Today.AddDays(-1), 10300);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        Assert.Equal(900, result.Price); // 100 * 9 days (SmallCar has no multiplier)
        Assert.Equal(9, result.Days);
        Assert.Equal(300, result.Km);
    }

    [Fact]
    public async Task Combi_PriceCalculation_IsCorrect()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B005", "DEF456", "2345678901", "Combi", DateTime.Today.AddDays(-3), 10000);
        var returnRequest = new ReturnRequest("B005", DateTime.Today, 10150);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        var expectedPrice = 100 * 3 * 1.3m + 2 * 150; // (baseDayRental * days * 1.3) + (baseKmPrice * km)
        Assert.Equal(expectedPrice, result.Price);
        Assert.Equal(3, result.Days);
        Assert.Equal(150, result.Km);
    }

    [Fact]
    public async Task Truck_PriceCalculation_IsCorrect()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B006", "XYZ789", "9876543210", "Truck", DateTime.Today.AddDays(-2), 10000);
        var returnRequest = new ReturnRequest("B006", DateTime.Today, 10200);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        var expectedPrice = 100 * 2 * 1.5m + 2 * 200 * 1.5m; // (baseDayRental * days * 1.5) + (baseKmPrice * km * 1.5)
        Assert.Equal(expectedPrice, result.Price);
        Assert.Equal(2, result.Days);
        Assert.Equal(200, result.Km);
    }

    [Fact]
    public async Task SameDayRental_PriceCalculation_IsCorrect()
    {
        // Arrange
        ClearRepository();
        var today = DateTime.Today;
        var pickupRequest = new PickupRequest("B007", "ABC123", "1234567890", "SmallCar", today, 10000);
        var returnRequest = new ReturnRequest("B007", today, 10100);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        Assert.Equal(100, result.Price); // Minimum 1 day charge
        Assert.Equal(1, result.Days); // Same day counts as 1 day
        Assert.Equal(100, result.Km);
    }

    [Fact]
    public async Task GetRentalById_ExistingRental_ReturnsRental()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B008", "ABC123", "1234567890", "SmallCar", DateTime.Today.AddDays(-1), 10000);
        var pickupResult = await _service.RegisterPickup(pickupRequest);

        // Act
        var result = await _service.GetRentalById(pickupResult.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pickupResult.Id, result.Id);
        Assert.Equal("B008", result.BookingNumber);
        Assert.False(result.IsReturned);
    }

    [Fact]
    public async Task GetRentalById_NonExistentId_ReturnsNull()
    {
        // Arrange
        ClearRepository();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _service.GetRentalById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllRentals_MultipleRentals_ReturnsAllRentals()
    {
        // Arrange
        ClearRepository();
        var request1 = new PickupRequest("B009", "ABC123", "1234567890", "SmallCar", DateTime.Today.AddDays(-2), 10000);
        var request2 = new PickupRequest("B010", "DEF456", "2345678901", "Combi", DateTime.Today.AddDays(-1), 15000);
        
        await _service.RegisterPickup(request1);
        await _service.RegisterPickup(request2);
        
        // Return one rental
        await _service.RegisterReturn(new ReturnRequest("B009", DateTime.Today, 10100));

        // Act
        var results = await _service.GetAllRentals();

        // Assert
        Assert.True(results.Count >= 2);
        
        var rental1 = results.FirstOrDefault(r => r.BookingNumber == "B009");
        var rental2 = results.FirstOrDefault(r => r.BookingNumber == "B010");
        
        Assert.NotNull(rental1);
        Assert.NotNull(rental2);
        Assert.True(rental1.IsReturned);
        Assert.False(rental2.IsReturned);
        Assert.True(rental1.Price > 0);
        Assert.Equal(0, rental2.Price);
    }

    [Fact]
    public async Task GetAllRentals_EmptyDatabase_ReturnsEmptyList()
    {
        // Arrange
        ClearRepository();
        
        // Act
        var results = await _service.GetAllRentals();

        // Assert
        Assert.NotNull(results);
        Assert.Empty(results); // Should be empty since we cleared the repository
        Assert.IsAssignableFrom<IReadOnlyList<RentalResult>>(results);
    }

    [Fact]
    public async Task RegisterReturn_ReturnedRental_CalculatesCorrectPrice()
    {
        // Arrange
        ClearRepository();
        var pickupRequest = new PickupRequest("B011", "ABC123", "1234567890", "Combi", DateTime.Today.AddDays(-7), 10000);
        var returnRequest = new ReturnRequest("B011", DateTime.Today.AddDays(-2), 10500);

        // Act
        await _service.RegisterPickup(pickupRequest);
        var result = await _service.RegisterReturn(returnRequest);

        // Assert
        Assert.Equal("B011", result.BookingNumber);
        Assert.Equal(5, result.Days); // 7 days ago to 2 days ago = 5 days
        Assert.Equal(500, result.Km);
        Assert.True(result.IsReturned);
        
        // Combi: (100 * 5 * 1.3) + (2 * 500) = 650 + 1000 = 1650
        Assert.Equal(1650, result.Price);
    }

    [Fact]
    public async Task RegisterPickup_AllCarCategories_CreatesCorrectRentals()
    {
        // Arrange
        ClearRepository();
        
        // Act
        var smallCarResult = await _service.RegisterPickup(
            new PickupRequest("B012", "ABC123", "1234567890", "SmallCar", DateTime.Today, 10000));
        
        var combiResult = await _service.RegisterPickup(
            new PickupRequest("B013", "DEF456", "2345678901", "Combi", DateTime.Today, 15000));
        
        var truckResult = await _service.RegisterPickup(
            new PickupRequest("B014", "XYZ789", "3456789012", "Truck", DateTime.Today, 20000));

        // Assert
        Assert.Equal("B012", smallCarResult.BookingNumber);
        Assert.Equal("B013", combiResult.BookingNumber);
        Assert.Equal("B014", truckResult.BookingNumber);
        
        Assert.All(new[] { smallCarResult, combiResult, truckResult }, result =>
        {
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.False(result.IsReturned);
            Assert.Equal(0, result.Price);
            Assert.Equal(0, result.Days);
        });
    }
}