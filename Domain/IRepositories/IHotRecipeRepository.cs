using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IHotRecipeRepository
    {
        Task IncrementViewCountAsync(Guid recipeId);
        Task<List<HotRecipe>> GetTopRecipesAsync(int limit=5);
        Task IncrementWishlistCountAsync(Guid recipeId);

    }
}
