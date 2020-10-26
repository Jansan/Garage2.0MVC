using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "VehicleModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 26, 16, 29, 35, 184, DateTimeKind.Local).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 26, 16, 29, 35, 191, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 26, 16, 29, 35, 191, DateTimeKind.Local).AddTicks(4511));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 26, 16, 29, 35, 191, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_MemberId",
                table: "VehicleModel",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Member_MemberId",
                table: "VehicleModel");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_MemberId",
                table: "VehicleModel");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "VehicleModel");

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 13, 54, 18, 167, DateTimeKind.Local).AddTicks(1896));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3631));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3702));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3713));
        }
    }
}
