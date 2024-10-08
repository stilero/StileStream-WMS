using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StileStream.Wms.Products.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OutboxMessages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CorrelationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: ""),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                Properties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OccurredOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7193)),
                IsProcessed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                ProcessedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7616))
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OutboxMessages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: ""),
                Manufacturer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(543)),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(1146))
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_OutboxMessages_CorrelationId",
            table: "OutboxMessages",
            column: "CorrelationId");

        migrationBuilder.CreateIndex(
            name: "IX_OutboxMessages_Id",
            table: "OutboxMessages",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_OutboxMessages_IsProcessed",
            table: "OutboxMessages",
            column: "IsProcessed");

        migrationBuilder.CreateIndex(
            name: "IX_OutboxMessages_TenantId",
            table: "OutboxMessages",
            column: "TenantId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_Category",
            table: "Products",
            column: "Category");

        migrationBuilder.CreateIndex(
            name: "IX_Products_Name",
            table: "Products",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_Products_Sku",
            table: "Products",
            column: "Sku",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Products_Status",
            table: "Products",
            column: "Status");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OutboxMessages");

        migrationBuilder.DropTable(
            name: "Products");
    }
}
