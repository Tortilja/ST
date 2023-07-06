using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST.Migrations
{
    /// <inheritdoc />
    public partial class AddEventToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Event_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event_ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Event_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
