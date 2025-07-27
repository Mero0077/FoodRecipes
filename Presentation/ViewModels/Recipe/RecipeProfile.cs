using Application.DTOs.Recipes;
using AutoMapper;
using Presentation.ViewModels.WishList;

namespace Presentation.ViewModels.Recipe
{
    public class RecipeProfile:Profile
    {
        public RecipeProfile()
        {
            CreateMap<Domain.Models.Recipe, RecipeVM>();
            CreateMap<RecipeVM, AddRecipeDTO>();
        }
    }
}
