using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGinDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Gins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: String.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Gins");
        }
    }
}
