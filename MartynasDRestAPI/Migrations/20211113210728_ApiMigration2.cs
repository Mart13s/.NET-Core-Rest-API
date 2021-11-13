using Microsoft.EntityFrameworkCore.Migrations;

namespace MartynasDRestAPI.Migrations
{
    public partial class ApiMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_reviewerid",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_reviewerid",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "reviewerid",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "reviewName",
                table: "reviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewName",
                table: "reviews");

            migrationBuilder.AddColumn<int>(
                name: "reviewerid",
                table: "reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_reviewerid",
                table: "reviews",
                column: "reviewerid");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_reviewerid",
                table: "reviews",
                column: "reviewerid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
