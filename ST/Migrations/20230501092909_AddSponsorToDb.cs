using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST.Migrations
{
    /// <inheritdoc />
    public partial class AddSponsorToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    Sponsor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sponsor_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sponsor_Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Sponsor_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sponsor");
        }
    }
}
