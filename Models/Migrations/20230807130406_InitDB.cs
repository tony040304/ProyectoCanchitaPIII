using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PITCH",
                columns: table => new
                {
                    IdPitch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PlaceName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PITCH__6C84D4CA694F1297", x => x.IdPitch);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    userpassword = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    Id_Turns = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_USERS = table.Column<int>(type: "int", nullable: false),
                    Id_PITCH = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Turns__E07395E57AB182F4", x => new { x.Id_Turns, x.Id_USERS, x.Id_PITCH });
                    table.ForeignKey(
                        name: "FK__Turns__Id_PITCH__4222D4EF",
                        column: x => x.Id_PITCH,
                        principalTable: "PITCH",
                        principalColumn: "IdPitch");
                    table.ForeignKey(
                        name: "FK__Turns__Id_USERS__412EB0B6",
                        column: x => x.Id_USERS,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turns_Id_PITCH",
                table: "Turns",
                column: "Id_PITCH");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_Id_USERS",
                table: "Turns",
                column: "Id_USERS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropTable(
                name: "PITCH");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
