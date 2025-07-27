using Application.DTOs.Recipes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Events
{
    public record AddHotRecipeToWishListEvent(Guid RecipeId) : INotification;


}
