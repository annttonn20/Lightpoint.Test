using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lightpoint.Test.Data.Migrations
{
    public partial class DefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "SET IDENTITY_INSERT [dbo].[ProductsEntity] ON " +
                "INSERT INTO[dbo].[ProductsEntity]([Id], [Description], [Name]) VALUES(1, N'It''s sweet enough', N'Sugar') " +
                "INSERT INTO[dbo].[ProductsEntity] " +
                "([Id], [Description], [Name]) VALUES(2, N'It''s salty enough', N'Salt') " +
                "SET IDENTITY_INSERT[dbo].[ProductsEntity] OFF");
            migrationBuilder.Sql(
                "SET IDENTITY_INSERT[dbo].[StoresEntity] ON " +
                "INSERT INTO[dbo].[StoresEntity]([Id], [Address], [Name], [WorkingHours]) VALUES(1, N'Minsk', N'Evroopt', N'8-23') " +
                "INSERT INTO[dbo].[StoresEntity] " +
                "([Id], [Address], [Name], [WorkingHours]) VALUES(2, N'Molodechno', N'ProStore', N'00-24') " +
                "SET IDENTITY_INSERT[dbo].[StoresEntity] OFF");
            migrationBuilder.Sql(
                "INSERT INTO[dbo].[ProductsAndStoresEntity]([ProductId], [StoreId]) VALUES(1, 1 )" +
                "INSERT INTO[dbo].[ProductsAndStoresEntity] " +
                "([ProductId], [StoreId]) VALUES(2, 1) " +
                "INSERT INTO[dbo].[ProductsAndStoresEntity] " +
                "([ProductId], [StoreId]) VALUES(1, 2) " +
                "INSERT INTO[dbo].[ProductsAndStoresEntity] ([ProductId], [StoreId]) VALUES(2, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsEntity] WHERE [Id] = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsEntity] WHERE [Id] = 2");
            migrationBuilder.Sql("DELETE FROM [dbo].[StoresEntity] WHERE [Id] = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[StoresEntity] WHERE [Id] = 2");
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsAndStoresEntity] WHERE [StoreId] = 2");
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsAndStoresEntity] WHERE [StoreId] = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsAndStoresEntity] WHERE [ProductId] = 2");
            migrationBuilder.Sql("DELETE FROM [dbo].[ProductsAndStoresEntity] WHERE [ProductId] = 1");
        }
    }
}
