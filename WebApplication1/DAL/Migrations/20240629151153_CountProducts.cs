using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class CountProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 29, 19, 11, 52, 947, DateTimeKind.Local).AddTicks(4922),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 25, 16, 2, 41, 966, DateTimeKind.Local).AddTicks(4273));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 25, 16, 2, 41, 966, DateTimeKind.Local).AddTicks(4273),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 29, 19, 11, 52, 947, DateTimeKind.Local).AddTicks(4922));
        }
    }
}
