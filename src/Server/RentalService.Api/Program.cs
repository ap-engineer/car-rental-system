using Microsoft.EntityFrameworkCore;
using RentalService.Application.Services.Interfaces;
using RentalService.Domain.Configuration;
using RentalService.Domain.Services.Implementation;
using RentalService.Domain.Services.Interfaces;
using RentalService.Infrastructure;
using RentalService.Infrastructure.Repositories;
using RentalService.Infrastructure.Repositories.Interfaces;
using MainRentalService = RentalService.Application.Services.Implementation.RentalService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configuration
builder.Services.Configure<RentalPricingOptions>(
    builder.Configuration.GetSection(RentalPricingOptions.SectionName)
);

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, MainRentalService>();
builder.Services.AddScoped<IRentalPricingService, RentalPricingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();