using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    BookingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PickupKm = table.Column<long>(type: "INTEGER", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReturnKm = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.BookingNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");
        }
    }
}
