using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoonyBin.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Patients");
        }
    }
}
