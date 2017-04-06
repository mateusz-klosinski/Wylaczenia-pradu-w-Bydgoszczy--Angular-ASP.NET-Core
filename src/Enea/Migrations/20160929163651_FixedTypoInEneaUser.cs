using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enea.Migrations
{
    public partial class FixedTypoInEneaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasActiveSubsciption",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "HasActiveSubscription",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasActiveSubscription",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "HasActiveSubsciption",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
