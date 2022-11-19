using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aYoTechTest.DAL.Migrations
{
    public partial class added_relationship_to_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_SupportedConversions_MeasuringUnits_SourceUnitId",
                table: "SupportedConversions",
                column: "SourceUnitId",
                principalTable: "MeasuringUnits",
                principalColumn: "MeasuringUnitId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportedConversions_MeasuringUnits_TargetUnitId",
                table: "SupportedConversions",
                column: "TargetUnitId",
                principalTable: "MeasuringUnits",
                principalColumn: "MeasuringUnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportedConversions_MeasuringUnits_SourceUnitId",
                table: "SupportedConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportedConversions_MeasuringUnits_TargetUnitId",
                table: "SupportedConversions");
        }
    }
}
