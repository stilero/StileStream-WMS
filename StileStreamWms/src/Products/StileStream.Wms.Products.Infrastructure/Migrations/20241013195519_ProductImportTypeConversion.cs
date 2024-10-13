using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StileStream.Wms.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductImportTypeConversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(3929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(3184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProductImports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(5913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4541));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductImports",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductImports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(5424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4286));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProductImportLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(8525),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(6060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductImportLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(7804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(5643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(1726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(1888));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(1123),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 975, DateTimeKind.Utc).AddTicks(9899));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(3929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(3109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(3184));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProductImports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(5913));

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ProductImports",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductImports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(4286),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProductImportLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(6060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(8525));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductImportLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(5643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(7804));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 976, DateTimeKind.Utc).AddTicks(1888),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 13, 19, 20, 11, 975, DateTimeKind.Utc).AddTicks(9899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 13, 19, 55, 19, 412, DateTimeKind.Utc).AddTicks(1123));
        }
    }
}
