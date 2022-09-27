using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaApi.Migrations
{
    public partial class AddUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6456), new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6478) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6489), new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6491) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6494), new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6496) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6499), new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6504), new DateTime(2022, 9, 18, 17, 50, 57, 346, DateTimeKind.Local).AddTicks(6505) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2646), new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2661) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2672), new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2674) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2677), new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2679) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2683), new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2684) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2688), new DateTime(2022, 9, 16, 17, 55, 39, 318, DateTimeKind.Local).AddTicks(2689) });
        }
    }
}
