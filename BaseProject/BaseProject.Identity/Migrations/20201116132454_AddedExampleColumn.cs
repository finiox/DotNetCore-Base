﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Identity.Migrations
{
    public partial class AddedExampleColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExampleColumn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExampleColumn",
                table: "AspNetUsers");
        }
    }
}