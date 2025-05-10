using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tixora.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "Id", "ContactEmail", "ContactPhone", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "info@comedyclub.com", "01032567599", "~/images/comedyLogo.jpg", "Comedy Club Egypt" },
                    { 2, "info@efa.com", "0223456789", "~/images/footlogo.jpg", "Egyptian Football Association" },
                    { 3, "contact@techsummit.org", "01012345678", "~/images/TechLogo.jpg", "Tech Summit Org" },
                    { 4, "info@science.org", "0229876543", "~/images/SciLogo.jpg", "Science Foundation" }
                });

            migrationBuilder.InsertData(
                table: "TicketCategories",
                columns: new[] { "Id", "Description", "Name", "PriceMultiplier" },
                values: new object[,]
                {
                    { 1, "VIP seating with complimentary drinks", "VIP", 2f },
                    { 2, "Regular seating", "Regular", 1f },
                    { 3, "Discounted ticket for students", "Student", 0.5f }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", 2000, "Cairo House" },
                    { 2, "Nasr City, Cairo", 75000, "Cairo International Stadium" },
                    { 3, "Alexandria", 1500, "Bibliotheca Alexandrina" },
                    { 4, "New Cairo", 500, "American University in Cairo" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "OrganizerId", "StartDate", "Title", "VenueId" },
                values: new object[,]
                {
                    { 1, "Entertainment", "An evening with Egypt's top comedians", "~/images/comedy-night.jpg", 1, new DateTime(2025, 6, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), "Stand-Up Comedy Night", 1 },
                    { 2, "Sports", "The classic Cairo football derby", "~/images/AlahlyVsZam.jpg", 2, new DateTime(2025, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Al Ahly vs Zamalek Derby", 2 },
                    { 3, "Entertainment", "Annual technology conference", "~/images/Trends.jpg", 3, new DateTime(2025, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Tech Trends 2025", 3 },
                    { 4, "Entertainment", "Scientific lecture on artificial intelligence", "~/images/Future.jpg", 4, new DateTime(2025, 8, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Future of AI", 4 },
                    { 5, "Sports", "The classic Cairo football derby", "~/images/PortovsAlahly.jpg", 2, new DateTime(2025, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Al Ahly vs FC Porto", 2 },
                    { 6, "Sports", "The classic Cairo football derby", "~/images/LivvsChe.jpg", 2, new DateTime(2025, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Liverpool vs chelsea", 2 },
                    { 7, "Sports", "The classic Cairo football derby", "~/images/barcavsreal.jpeg", 2, new DateTime(2025, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Barcelona vs Real Madrid", 2 },
                    { 8, "Sports", "The classic Cairo football derby", "~/images/Manvscity.jpeg", 2, new DateTime(2025, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Manchester United vs Manchester City", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AvailableQuantity", "EventId", "Price", "Status", "TicketCategoryId" },
                values: new object[,]
                {
                    { 1, 50, 1, 500m, 0, 1 },
                    { 2, 150, 1, 300m, 0, 2 },
                    { 3, 100, 1, 150m, 0, 3 },
                    { 4, 500, 2, 1000m, 0, 1 },
                    { 5, 5000, 2, 500m, 0, 2 },
                    { 6, 2000, 2, 250m, 0, 3 },
                    { 7, 30, 3, 800m, 0, 1 },
                    { 8, 200, 3, 400m, 0, 2 },
                    { 9, 100, 3, 280m, 0, 3 },
                    { 10, 300, 4, 200m, 0, 2 },
                    { 11, 150, 4, 100m, 0, 3 },
                    { 12, 500, 5, 1000m, 0, 1 },
                    { 13, 5000, 5, 500m, 0, 2 },
                    { 14, 2000, 5, 250m, 0, 3 },
                    { 15, 500, 6, 1000m, 0, 1 },
                    { 16, 5000, 6, 500m, 0, 2 },
                    { 17, 2000, 6, 250m, 0, 3 },
                    { 18, 500, 7, 1000m, 0, 1 },
                    { 19, 5000, 7, 500m, 0, 2 },
                    { 20, 2000, 7, 250m, 0, 3 },
                    { 21, 500, 8, 1000m, 0, 1 },
                    { 22, 5000, 8, 500m, 0, 2 },
                    { 23, 2000, 8, 250m, 0, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
