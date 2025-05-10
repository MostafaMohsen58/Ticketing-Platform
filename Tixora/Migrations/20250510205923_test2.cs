using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tixora.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "images/comedy-night.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "images/AlahlyVsZam.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "images/Trends.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "images/Future.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "images/PortovsAlahly.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "images/LivvsChe.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "images/barcavsreal.jpeg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "images/Manvscity.jpeg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LogoUrl",
                value: "comedyLogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoUrl",
                value: "footlogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LogoUrl",
                value: "TechLogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LogoUrl",
                value: "SciLogo.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/images/comedy-night.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/images/AlahlyVsZam.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/images/Trends.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/images/Future.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/images/PortovsAlahly.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/images/LivvsChe.jpg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "~/images/barcavsreal.jpeg");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "~/images/Manvscity.jpeg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LogoUrl",
                value: "~/images/comedyLogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoUrl",
                value: "~/images/footlogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LogoUrl",
                value: "~/images/TechLogo.jpg");

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LogoUrl",
                value: "~/images/SciLogo.jpg");
        }
    }
}
