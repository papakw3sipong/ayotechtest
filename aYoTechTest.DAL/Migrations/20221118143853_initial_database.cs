using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace aYoTechTest.DAL.Migrations
{
    public partial class initial_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasuringUnits",
                columns: table => new
                {
                    MeasuringUnitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitType = table.Column<byte>(type: "smallint", nullable: false),
                    MetricUnitDesc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AuthorisedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    AuthorisedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastUpdatedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringUnits", x => x.MeasuringUnitId);
                });

            migrationBuilder.CreateTable(
                name: "SupportedConversions",
                columns: table => new
                {
                    SupportedConversionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversionType = table.Column<byte>(type: "smallint", nullable: false),
                    SourceUnitId = table.Column<int>(type: "integer", nullable: false),
                    TargetUnitId = table.Column<int>(type: "integer", nullable: false),
                    SourceUnitValue = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 1m),
                    Multiplier = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0.0m),
                    CreatedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AuthorisedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    AuthorisedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastUpdatedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedById = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedConversions", x => x.SupportedConversionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_AuthorisedAt",
                table: "MeasuringUnits",
                column: "AuthorisedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_AuthorisedById",
                table: "MeasuringUnits",
                column: "AuthorisedById");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_CreatedAt",
                table: "MeasuringUnits",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_CreatedById",
                table: "MeasuringUnits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_DeletedAt",
                table: "MeasuringUnits",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_DeletedById",
                table: "MeasuringUnits",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_LastUpdatedAt",
                table: "MeasuringUnits",
                column: "LastUpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringUnits_LastUpdatedById",
                table: "MeasuringUnits",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_AuthorisedAt",
                table: "SupportedConversions",
                column: "AuthorisedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_AuthorisedById",
                table: "SupportedConversions",
                column: "AuthorisedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_CreatedAt",
                table: "SupportedConversions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_CreatedById",
                table: "SupportedConversions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_DeletedAt",
                table: "SupportedConversions",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_DeletedById",
                table: "SupportedConversions",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_LastUpdatedAt",
                table: "SupportedConversions",
                column: "LastUpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_LastUpdatedById",
                table: "SupportedConversions",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_SourceUnitId",
                table: "SupportedConversions",
                column: "SourceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_SourceUnitValue",
                table: "SupportedConversions",
                column: "SourceUnitValue");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedConversions_TargetUnitId",
                table: "SupportedConversions",
                column: "TargetUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasuringUnits");

            migrationBuilder.DropTable(
                name: "SupportedConversions");
        }
    }
}
