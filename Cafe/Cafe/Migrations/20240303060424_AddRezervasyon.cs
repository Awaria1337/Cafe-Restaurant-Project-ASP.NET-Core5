using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cafe.Migrations
{
    public partial class AddRezervasyon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervasyons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sayi = table.Column<int>(type: "int", nullable: false),
                    Saat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervasyons");
        }
    }
}
