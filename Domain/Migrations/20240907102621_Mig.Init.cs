using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class MigInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mis");

            migrationBuilder.EnsureSchema(
                name: "fin");

            migrationBuilder.EnsureSchema(
                name: "shp");

            migrationBuilder.CreateTable(
                name: "tbCustomers",
                schema: "mis",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCustomers", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "tbProducts",
                schema: "shp",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProducts", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "tbSellers",
                schema: "mis",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSellers", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "tbSellLines",
                schema: "shp",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSellLines", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "tbInvoices",
                schema: "fin",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvStatus = table.Column<int>(type: "int", nullable: false),
                    FkSellLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkSellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbInvoices", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_tbInvoices_tbCustomers_FkCustomerId",
                        column: x => x.FkCustomerId,
                        principalSchema: "mis",
                        principalTable: "tbCustomers",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbInvoices_tbSellLines_FkSellLineId",
                        column: x => x.FkSellLineId,
                        principalSchema: "shp",
                        principalTable: "tbSellLines",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbInvoices_tbSellers_FkSellerId",
                        column: x => x.FkSellerId,
                        principalSchema: "mis",
                        principalTable: "tbSellers",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbSellLineProducts",
                schema: "shp",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkSellLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSellLineProducts", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_tbSellLineProducts_tbProducts_FkProductId",
                        column: x => x.FkProductId,
                        principalSchema: "shp",
                        principalTable: "tbProducts",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbSellLineProducts_tbSellLines_FkSellLineId",
                        column: x => x.FkSellLineId,
                        principalSchema: "shp",
                        principalTable: "tbSellLines",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbSellLineSellers",
                schema: "shp",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkSellLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkSellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSellLineSellers", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_tbSellLineSellers_tbSellLines_FkSellLineId",
                        column: x => x.FkSellLineId,
                        principalSchema: "shp",
                        principalTable: "tbSellLines",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbSellLineSellers_tbSellers_FkSellerId",
                        column: x => x.FkSellerId,
                        principalSchema: "mis",
                        principalTable: "tbSellers",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbInvoiceDetails",
                schema: "fin",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<long>(type: "bigint", nullable: false),
                    FkProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbInvoiceDetails", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_tbInvoiceDetails_tbInvoices_FkInvoiceId",
                        column: x => x.FkInvoiceId,
                        principalSchema: "fin",
                        principalTable: "tbInvoices",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbInvoiceDetails_tbProducts_FkProductId",
                        column: x => x.FkProductId,
                        principalSchema: "shp",
                        principalTable: "tbProducts",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbDiscounts",
                schema: "fin",
                columns: table => new
                {
                    PkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    FkInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkInvoiceDetialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbDiscounts", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_tbDiscounts_tbInvoiceDetails_FkInvoiceDetialId",
                        column: x => x.FkInvoiceDetialId,
                        principalSchema: "fin",
                        principalTable: "tbInvoiceDetails",
                        principalColumn: "PkId");
                    table.ForeignKey(
                        name: "FK_tbDiscounts_tbInvoices_FkInvoiceId",
                        column: x => x.FkInvoiceId,
                        principalSchema: "fin",
                        principalTable: "tbInvoices",
                        principalColumn: "PkId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbDiscounts_FkInvoiceDetialId",
                schema: "fin",
                table: "tbDiscounts",
                column: "FkInvoiceDetialId");

            migrationBuilder.CreateIndex(
                name: "IX_tbDiscounts_FkInvoiceId",
                schema: "fin",
                table: "tbDiscounts",
                column: "FkInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbInvoiceDetails_FkInvoiceId",
                schema: "fin",
                table: "tbInvoiceDetails",
                column: "FkInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbInvoiceDetails_FkProductId",
                schema: "fin",
                table: "tbInvoiceDetails",
                column: "FkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbInvoices_FkCustomerId",
                schema: "fin",
                table: "tbInvoices",
                column: "FkCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbInvoices_FkSellerId",
                schema: "fin",
                table: "tbInvoices",
                column: "FkSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbInvoices_FkSellLineId",
                schema: "fin",
                table: "tbInvoices",
                column: "FkSellLineId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSellLineProducts_FkProductId",
                schema: "shp",
                table: "tbSellLineProducts",
                column: "FkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSellLineProducts_FkSellLineId",
                schema: "shp",
                table: "tbSellLineProducts",
                column: "FkSellLineId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSellLineSellers_FkSellerId",
                schema: "shp",
                table: "tbSellLineSellers",
                column: "FkSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSellLineSellers_FkSellLineId",
                schema: "shp",
                table: "tbSellLineSellers",
                column: "FkSellLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbDiscounts",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "tbSellLineProducts",
                schema: "shp");

            migrationBuilder.DropTable(
                name: "tbSellLineSellers",
                schema: "shp");

            migrationBuilder.DropTable(
                name: "tbInvoiceDetails",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "tbInvoices",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "tbProducts",
                schema: "shp");

            migrationBuilder.DropTable(
                name: "tbCustomers",
                schema: "mis");

            migrationBuilder.DropTable(
                name: "tbSellLines",
                schema: "shp");

            migrationBuilder.DropTable(
                name: "tbSellers",
                schema: "mis");
        }
    }
}
