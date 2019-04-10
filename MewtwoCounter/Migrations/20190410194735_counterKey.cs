using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MewtwoCounter.Migrations
{
    public partial class counterKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CounterKey",
                table: "Counters",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Counters_CounterKey",
                table: "Counters",
                column: "CounterKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Counters_CounterKey",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "CounterKey",
                table: "Counters");
        }
    }
}
