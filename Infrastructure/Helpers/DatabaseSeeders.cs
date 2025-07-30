using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class DatabaseSeeders
    {
        public static void SeedFeatureData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feature>().HasData(
                new Feature() { Id = new Guid(Constants.GetAllRolesFeature), FeatureName = FeatureCode.GetAllRoles.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.DeleteRoleFeature), FeatureName = FeatureCode.DeleteRole.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.UpdateRoleFeature), FeatureName = FeatureCode.UpdateRole.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.CreateRoleFeature), FeatureName = FeatureCode.CreateRole.ToString(), CreatedAt = null },

                new Feature() { Id = new Guid(Constants.GetAllCategoriesFeature), FeatureName = FeatureCode.GetAllCategories.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.CreateCategoryFeature), FeatureName = FeatureCode.CreateCategory.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.UpdateCategoryFeature), FeatureName = FeatureCode.UpdateCategory.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.DeleteCategoryFeature), FeatureName = FeatureCode.DeleteCategory.ToString(), CreatedAt = null },

                new Feature() { Id = new Guid(Constants.AddRecipeFeature), FeatureName = FeatureCode.AddRecipe.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.GetHotRecipesFeature), FeatureName = FeatureCode.GetHotRecipe.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.GetRecipeFeature), FeatureName = FeatureCode.GetRecipe.ToString(), CreatedAt = null },

                new Feature() { Id = new Guid(Constants.AddRecipeToWishlistFeature), FeatureName = FeatureCode.AddRecipeToWishlist.ToString(), CreatedAt = null },
                new Feature() { Id = new Guid(Constants.RemoveRecipeFromWishlistFeature), FeatureName = FeatureCode.RemoveRecipeFromWishlist.ToString(), CreatedAt = null },


                new Feature() { Id = new Guid(Constants.AssignFeatureToRoleFeature), FeatureName = FeatureCode.AssignFeatureToRole.ToString(), CreatedAt = null },

                new Feature() { Id = new Guid(Constants.CreateWishListFeature), FeatureName = FeatureCode.CreateWishList.ToString(), CreatedAt = null }


            );
        }

        public static void SeedNoneRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Models.Role>().HasData(
                new Domain.Models.Role() { Id=new Guid(Constants.NoneRole) ,Name="None",CreatedAt=null}
            );
        }
    }
}
