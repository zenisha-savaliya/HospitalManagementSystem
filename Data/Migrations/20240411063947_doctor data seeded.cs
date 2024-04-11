using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class doctordataseeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "ContactNumber", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "PostalCode", "Specialist", "UserId" },
                values: new object[] { 1, "1234567890", new DateTime(2003, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "zenishasavaliya96@gmail.com", "zenisha", "Female", "savaliya", "e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446", null, "Brain Surgery", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);
        }
    }
}
