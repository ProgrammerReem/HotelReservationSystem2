using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Text = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Imagepath = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Desc = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Loc = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rating = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Count = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pageContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Text = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AboutUs = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AboutUsTitle = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AboutUsImagePath = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ImagePath = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    phone = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Balance = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CardID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CardCVV = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    roomNumber = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RoomType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PriceByNight = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Desc = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ImagePath = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    HotelId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.id);
                    table.ForeignKey(
                        name: "FK_room_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CardId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cvv = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Balance = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bankAccounts_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RoomId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "residents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RoomId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_residents_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_residents_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    text = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    userId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    roomId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    HotelId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testimonials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_testimonials_hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_testimonials_room_roomId",
                        column: x => x.roomId,
                        principalTable: "room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_testimonials_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Price = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RoomId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userTransactions_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTransactions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bankAccounts_UserId",
                table: "bankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_RoomId",
                table: "reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_UserID",
                table: "reservations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_residents_RoomId",
                table: "residents",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_residents_UserID",
                table: "residents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_room_HotelId",
                table: "room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_testimonials_HotelId",
                table: "testimonials",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_testimonials_roomId",
                table: "testimonials",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_testimonials_userId",
                table: "testimonials",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userTransactions_RoomId",
                table: "userTransactions",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_userTransactions_UserID",
                table: "userTransactions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankAccounts");

            migrationBuilder.DropTable(
                name: "contactUs");

            migrationBuilder.DropTable(
                name: "pageContent");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "residents");

            migrationBuilder.DropTable(
                name: "testimonials");

            migrationBuilder.DropTable(
                name: "userTransactions");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "hotels");
        }
    }
}
