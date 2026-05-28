using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoonyBin.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GivenNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
