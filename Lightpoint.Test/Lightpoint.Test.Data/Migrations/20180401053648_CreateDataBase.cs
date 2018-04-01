using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lightpoint.Test.Data.Migrations
{
    public partial class CreateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoresEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    WorkingHours = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsAndStoresEntity",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsAndStoresEntity", x => new { x.ProductId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_ProductsAndStoresEntity_ProductsEntity_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsAndStoresEntity_StoresEntity_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoresEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsAndStoresEntity_StoreId",
                table: "ProductsAndStoresEntity",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsEntity_Name",
                table: "ProductsEntity",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsAndStoresEntity");

            migrationBuilder.DropTable(
                name: "ProductsEntity");

            migrationBuilder.DropTable(
                name: "StoresEntity");
        }
    }
}
