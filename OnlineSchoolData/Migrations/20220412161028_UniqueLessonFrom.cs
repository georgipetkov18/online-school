using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolData.Migrations
{
    public partial class UniqueLessonFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lessons_From",
                table: "Lessons",
                column: "From",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_From",
                table: "Lessons");
        }
    }
}
