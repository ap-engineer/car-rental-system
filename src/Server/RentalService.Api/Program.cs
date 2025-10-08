using RentalService.Application.Services.Interfaces;
using RentalService.Domain.Services.Implementation;
using RentalService.Domain.Services.Interfaces;
using MainRentalService = RentalService.Application.Services.Implementation.RentalService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<IRentalPricingService, RentalPricingService>();
builder.Services.AddSingleton<IRentalService, MainRentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();