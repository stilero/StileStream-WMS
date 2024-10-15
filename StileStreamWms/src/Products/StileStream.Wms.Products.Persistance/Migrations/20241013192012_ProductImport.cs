using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StileStream.Wms.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductImport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(1146));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(543));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(1888),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7616));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 975, DateTimeKind.Utc).AddTicks(9899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7193));

            migrationBuilder.CreateTable(
                name: "ProductImports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4286)),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4541))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImportLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    ProductSku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductManufacturer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    ProductCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    ProductStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(5643)),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "system"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(6060))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImportLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImportLines_ProductImports_ProductImportId",
                        column: x => x.ProductImportId,
                        principalTable: "ProductImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImportLines_ProductImportId",
                table: "ProductImportLines",
                column: "ProductImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImportLines_Status",
                table: "ProductImportLines",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImports_Status",
                table: "ProductImports",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImportLines");

            migrationBuilder.DropTable(
                name: "ProductImports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(1146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 103, DateTimeKind.Utc).AddTicks(543),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7616),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(1888));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 8, 20, 6, 26, 102, DateTimeKind.Utc).AddTicks(7193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 975, DateTimeKind.Utc).AddTicks(9899));
        }
    }
}
