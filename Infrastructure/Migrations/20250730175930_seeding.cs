using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "FeatureName", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("31279ac3-f37f-4955-8c49-8b105b39dd4e"), null, null, "UpdateCategory", false, null },
                    { new Guid("67e888ec-5a62-4df7-8417-c558bbf7b650"), null, null, "DeleteCategory", false, null },
                    { new Guid("8b92c59b-28f4-41f0-81ad-f30819fcf6a3"), null, null, "CreateCategory", false, null },
                    { new Guid("8c24e1f4-47f1-4e62-8a30-1f4fd963a9a0"), null, null, "AddRecipeToWishlist", false, null },
                    { new Guid("a2f87429-2b1e-41e7-9f8f-7da5b6fd0e01"), null, null, "GetRecipe", false, null },
                    { new Guid("b8efa0b6-13c2-45b7-a926-16bc157cb1da"), null, null, "GetHotRecipe", false, null },
                    { new Guid("bfd1c8e7-8a29-4a49-8a56-0f9f81a91f39"), null, null, "RemoveRecipeFromWishlist", false, null },
                    { new Guid("bfef7e58-5e3e-4e4e-9df0-8fc8ac20ffae"), null, null, "AssignFeatureToRole", false, null },
                    { new Guid("cf78513c-3641-4c6f-9b4f-0b6a51a5683c"), null, null, "AddRecipe", false, null },
                    { new Guid("e3d92c33-4b0d-44a8-9487-1db3bff6fc01"), null, null, "GetAllCategories", false, null },
                    { new Guid("e9d8a6d6-7b3a-4e8b-a4f9-9cbdcdffbd3a"), null, null, "CreateWishList", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("31279ac3-f37f-4955-8c49-8b105b39dd4e"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("67e888ec-5a62-4df7-8417-c558bbf7b650"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("8b92c59b-28f4-41f0-81ad-f30819fcf6a3"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("8c24e1f4-47f1-4e62-8a30-1f4fd963a9a0"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a2f87429-2b1e-41e7-9f8f-7da5b6fd0e01"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("b8efa0b6-13c2-45b7-a926-16bc157cb1da"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("bfd1c8e7-8a29-4a49-8a56-0f9f81a91f39"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("bfef7e58-5e3e-4e4e-9df0-8fc8ac20ffae"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cf78513c-3641-4c6f-9b4f-0b6a51a5683c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e3d92c33-4b0d-44a8-9487-1db3bff6fc01"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e9d8a6d6-7b3a-4e8b-a4f9-9cbdcdffbd3a"));
        }
    }
}
