using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleManager.Migrations
{
    public partial class AddContentMarkdownToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentMarkdown",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentMarkdown",
                table: "Article");
        }
    }
}
