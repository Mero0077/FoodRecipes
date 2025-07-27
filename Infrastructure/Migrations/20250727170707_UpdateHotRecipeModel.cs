using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotRecipeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "HotRecipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HotRecipes_RecipeId",
                table: "HotRecipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotRecipes_Recipes_RecipeId",
                table: "HotRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotRecipes_Recipes_RecipeId",
                table: "HotRecipes");

            migrationBuilder.DropIndex(
                name: "IX_HotRecipes_RecipeId",
                table: "HotRecipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "HotRecipes");
        }
    }
}
