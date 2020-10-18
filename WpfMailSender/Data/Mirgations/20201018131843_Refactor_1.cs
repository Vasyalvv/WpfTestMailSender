using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfMailSender.Data.Mirgations
{
    public partial class Refactor_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulerTaskId",
                table: "Recipients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_SchedulerTaskId",
                table: "Recipients",
                column: "SchedulerTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_SchedulerTasks_SchedulerTaskId",
                table: "Recipients",
                column: "SchedulerTaskId",
                principalTable: "SchedulerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_SchedulerTasks_SchedulerTaskId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_SchedulerTaskId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "SchedulerTaskId",
                table: "Recipients");
        }
    }
}
