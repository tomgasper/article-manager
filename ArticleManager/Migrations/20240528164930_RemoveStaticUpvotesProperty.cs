using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleManager.Migrations
{
    public partial class RemoveStaticUpvotesProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Article");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
