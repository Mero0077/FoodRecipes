using Application.DTOs.Recipes;
using AutoMapper;
using Domain.Models;
using System;

namespace Presentation.ViewModels.RecipeWishList
{
    public class RecipeWisListProfile:Profile
    {
        public RecipeWisListProfile()
        {
            CreateMap<AddRecipewishListVM, WishListRecipeDTO>().ReverseMap();
            CreateMap<WishListRecipeDTO, WishListRecipe>().ReverseMap();
            CreateMap<WishListRecipe, AddRecipewishListVM>();
        }
    }
}
