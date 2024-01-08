using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCSharpSeventh.Migrations
{
    /// <inheritdoc />
    public partial class RenameIPAdressToIPAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IPAdress",
                table: "Servers",
                newName: "IPAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IPAddress",
                table: "Servers",
                newName: "IPAdress");
        }
    }
}
