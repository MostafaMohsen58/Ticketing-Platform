using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tixora.Migrations
{
    /// <inheritdoc />
    public partial class testToWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Organizers_OrganizerId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Tickets",
                newName: "OrginzierId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_OrganizerId",
                table: "Tickets",
                newName: "IX_Tickets_OrginzierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Organizers_OrginzierId",
                table: "Tickets",
                column: "OrginzierId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Organizers_OrginzierId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "OrginzierId",
                table: "Tickets",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_OrginzierId",
                table: "Tickets",
                newName: "IX_Tickets_OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Organizers_OrganizerId",
                table: "Tickets",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
