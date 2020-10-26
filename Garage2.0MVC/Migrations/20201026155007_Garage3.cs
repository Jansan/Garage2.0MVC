using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class Garage3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "VehicleModel",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleNum",
                table: "VehicleModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "VehicleModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 26, 16, 50, 6, 803, DateTimeKind.Local).AddTicks(2325), 0 });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5826), 0 });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5926), 0 });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5944), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleTypeId",
                table: "VehicleModel",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleType_VehicleTypeId",
                table: "VehicleModel",
                column: "VehicleTypeId",
                principalTable: "VehicleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleType_VehicleTypeId",
                table: "VehicleModel");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_VehicleTypeId",
                table: "VehicleModel");

            migrationBuilder.DropColumn(
                name: "VehicleNum",
                table: "VehicleModel");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "VehicleModel");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "VehicleModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 19, 14, 11, 40, 371, DateTimeKind.Local).AddTicks(4207), null });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9796), null });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9935), null });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "MemberId" },
                values: new object[] { new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9947), null });

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
