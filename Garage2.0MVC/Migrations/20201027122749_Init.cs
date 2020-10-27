using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ParkingSpace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Capacity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    RegNum = table.Column<string>(maxLength: 6, nullable: false),
                    Color = table.Column<string>(maxLength: 20, nullable: false),
                    Brand = table.Column<string>(maxLength: 25, nullable: false),
                    Model = table.Column<string>(maxLength: 25, nullable: false),
                    NumWheels = table.Column<int>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModel_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleType_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModelParkingSpace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleModelId = table.Column<int>(nullable: false),
                    ParkingSpaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModelParkingSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModelParkingSpace_ParkingSpace_ParkingSpaceId",
                        column: x => x.ParkingSpaceId,
                        principalTable: "ParkingSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleModelParkingSpace_VehicleModel_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VehicleType",
                columns: new[] { "Id", "Capacity", "Type" },
                values: new object[,]
                {
                    { 1, 1.0, 0 },
                    { 2, 0.29999999999999999, 1 },
                    { 3, 2.0, 2 },
                    { 4, 2.0, 3 },
                    { 5, 3.0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_MemberId",
                table: "VehicleModel",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleTypeId",
                table: "VehicleModel",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModelParkingSpace_ParkingSpaceId",
                table: "VehicleModelParkingSpace",
                column: "ParkingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModelParkingSpace_VehicleModelId",
                table: "VehicleModelParkingSpace",
                column: "VehicleModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleModelParkingSpace");

            migrationBuilder.DropTable(
                name: "ParkingSpace");

            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "VehicleType");
        }
    }
}
