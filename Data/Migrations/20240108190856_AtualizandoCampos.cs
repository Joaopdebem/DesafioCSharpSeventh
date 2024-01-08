using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCSharpSeventh.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BinaryContent",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "IPPort",
                table: "Servers",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "IPAddress",
                table: "Servers",
                newName: "Ip");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Videos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Videos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Videos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SizeInBytes",
                table: "Videos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Servers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Servers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Servers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "SizeInBytes",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Servers");

            migrationBuilder.RenameColumn(
                name: "Port",
                table: "Servers",
                newName: "IPPort");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "Servers",
                newName: "IPAddress");

            migrationBuilder.AddColumn<byte[]>(
                name: "BinaryContent",
                table: "Videos",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
