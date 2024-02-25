using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDirectiryOfDepartments.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectoryUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentDirectoryUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectoryUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectoryUnits_DirectoryUnits_ParentDirectoryUnitId",
                        column: x => x.ParentDirectoryUnitId,
                        principalTable: "DirectoryUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUnits_ParentDirectoryUnitId",
                table: "DirectoryUnits",
                column: "ParentDirectoryUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectoryUnits");
        }
    }
}
